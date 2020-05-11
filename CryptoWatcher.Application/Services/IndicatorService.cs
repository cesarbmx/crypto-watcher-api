using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.ModelBuilders;
using CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Persistence.Repositories;
using CryptoWatcher.Domain.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class IndicatorService
    {
        private readonly DbContext _dbContext;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<IndicatorDependency> _indicatorDependencyRepository;
        private readonly ILogger<IndicatorService> _logger;
        private readonly IMapper _mapper;

        public IndicatorService(
            DbContext dbContext,
            IRepository<Indicator> indicatorRepository,
            Repository<IndicatorDependency> indicatorDependencyRepository,
            IRepository<User> userRepository,
            ILogger<IndicatorService> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _indicatorRepository = indicatorRepository;
            _indicatorDependencyRepository = indicatorDependencyRepository;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<IndicatorResponse>> GetAllIndicators(string userId, IndicatorType indicatorType)
        {
            // Get user
            var user = await _userRepository.GetSingle(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all indicators
            var indicators = await _indicatorRepository.GetAll(IndicatorExpression.IndicatorFilter(indicatorType, null, userId));

            // Get all indicator dependencies
            var indicatorDependencies = await _indicatorDependencyRepository.GetAll();

            // Build indicator dependencies
            IndicatorBuilder.BuildDependencies(indicators, indicatorDependencies);

            // Response
            var response = _mapper.Map<List<IndicatorResponse>>(indicators);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> GetIndicator(string indicatorId)
        {
            // Get indicator
            var indicator = await _indicatorRepository.GetSingle(indicatorId);

            // Throw NotFoundException if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Get all indicator dependencies
            var indicatorDependencies = await _indicatorDependencyRepository.GetAll(IndicatorDependencyExpression.IndicatorDependencyFilter(indicatorId));

            // Set indicator dependencies
            indicator.SetDependencies(indicatorDependencies);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> AddIndicator(AddIndicatorRequest request)
        {
            // Get indicator
            var indicator = await _indicatorRepository.GetSingle(IndicatorExpression.Indicator(request.IndicatorId));

            // Throw ConflictException if it exists
            if (indicator != null) throw new ConflictException(IndicatorMessage.IndicatorWithSameIdAlreadyExists);

            // Check uniqueness
            indicator = await _indicatorRepository.GetSingle(IndicatorExpression.IndicatorUnique(request.Name));

            // Throw ConflictException if it exists
            if (indicator != null) throw new ConflictException(IndicatorMessage.IndicatorWithSameNameAlreadyExists);

            // Get dependencies
            var dependencies = await GetDependencies(request.Dependencies);

            // Build dependency level
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(dependencies);

            // Build new indicator dependencies
            var indicatorDependencies = IndicatorDependencyBuilder.BuildIndicatorDependencies(request.IndicatorId, dependencies);

            // Create
            indicator = new Indicator(
                request.IndicatorId,
                request.IndicatorType,
                request.UserId,
                request.Name,
                request.Description,
                request.Formula,
                indicatorDependencies,
                dependencyLevel,
                DateTime.Now);

            // Add
            _indicatorRepository.Add(indicator);

            // Save
            await _dbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> UpdateIndicator(UpdateIndicatorRequest request)
        {
            // Get indicator
            var indicator = await _indicatorRepository.GetSingle(request.IndicatorId);

            // Throw NotFoundException if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Get dependencies
            var newDependencies = await GetDependencies(request.Dependencies);

            // Build new indicator dependencies
            var newIndicatorDependencies = IndicatorDependencyBuilder.BuildIndicatorDependencies(indicator.IndicatorId, newDependencies);

            // Get current indicator dependencies 
            var currentIndicatorDependencies = await _indicatorDependencyRepository.GetAll(IndicatorDependencyExpression.IndicatorDependencyFilter(indicator.IndicatorId));

            // Update dependencies
            _indicatorDependencyRepository.UpdateCollection(currentIndicatorDependencies, newIndicatorDependencies);

            // Update indicator
            indicator.Update(request.Name, request.Description, request.Formula);

            // Update
            _indicatorRepository.Update(indicator);

            // Set dependencies
            indicator.SetDependencies(newIndicatorDependencies);

            // Save
            await _dbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }

        public async Task<List<Indicator>> UpdateIndicatorDependencies()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get all indicators
            var indicators = await _indicatorRepository.GetAll();

            // Get all indicator dependencies
            var indicatorDependencies = await _indicatorDependencyRepository.GetAll();

            // Build indicator dependencies
            IndicatorBuilder.BuildDependencies(indicators, indicatorDependencies);

            // Build dependency levels
            IndicatorBuilder.BuildDependencyLevels(indicators, indicatorDependencies);

            // Update
            _indicatorRepository.UpdateRange(indicators);

            // Save
            await _dbContext.SaveChangesAsync();

            // Build max dependency level
            var maxDependencyLevel = IndicatorBuilder.BuildMaxDependencyLevel(indicators);

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("UpdateIndicatorDependencies", new
            {
                MaxLevel = maxDependencyLevel,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return
            return indicators;
        }

        private async Task<List<Indicator>> GetDependencies(string[] dependencyIds)
        {
            var dependencies = new List<Indicator>();
            foreach (var dependencyId in dependencyIds)
            {
                // Get indicator
                var dependency = await _indicatorRepository.GetSingle(IndicatorExpression.Indicator(dependencyId));

                // Throw ValidationException if it does not exist
                if (dependency == null) throw new ValidationException(string.Format(IndicatorMessage.DepenedencyNotFound, dependencyId));

                // Add
                dependencies.Add(dependency);
            }

            // Return
            return dependencies;
        }
    }
}
