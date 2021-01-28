using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Logging.Extensions;
using CesarBmx.Shared.Persistence.Extensions;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class IndicatorService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<IndicatorService> _logger;
        private readonly IMapper _mapper;

        public IndicatorService(
            MainDbContext mainDbContext,
            ILogger<IndicatorService> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Responses.Indicator>> GetAllIndicators(string userId, IndicatorType indicatorType)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all indicators
            var indicators = await _mainDbContext.Indicators
                .Include(x=>x.Dependencies)
                .Where(IndicatorExpression.Filter(indicatorType, null, userId)).ToListAsync();

            // Response
            var response = _mapper.Map<List<Responses.Indicator>>(indicators);

            // Return
            return response;
        }
        public async Task<Responses.Indicator> GetIndicator(string indicatorId)
        {
            // Get indicator
            var indicator = await _mainDbContext.Indicators
                .Include(x => x.Dependencies)
                .FirstOrDefaultAsync(x=>x.IndicatorId == indicatorId);

            // Throw NotFound if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Response
            var response = _mapper.Map<Responses.Indicator>(indicator);

            // Return
            return response;
        }
        public async Task<Responses.Indicator> AddIndicator(AddIndicator request)
        {
            // Get indicator
            var indicator = await _mainDbContext.Indicators
                .Include(x => x.Dependencies)
                .FirstOrDefaultAsync(x => x.IndicatorId == request.IndicatorId);

            // Throw ConflictException if it exists
            if (indicator != null) throw new ConflictException(IndicatorMessage.IndicatorWithSameIdAlreadyExists);

            // Check uniqueness
            indicator = await _mainDbContext.Indicators
                .Include(x => x.Dependencies)
                .FirstOrDefaultAsync(IndicatorExpression.Unique(request.Name));

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
            _mainDbContext.Indicators.Add(indicator);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<Responses.Indicator>(indicator);

            // Return
            return response;
        }
        public async Task<Responses.Indicator> UpdateIndicator(UpdateIndicator request)
        {
            // Get indicator
            var indicator = await _mainDbContext.Indicators
                .Include(x => x.Dependencies)
                .FirstOrDefaultAsync(x => x.IndicatorId == request.IndicatorId);

            // Throw NotFound if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Get dependencies
            var newDependencies = await GetDependencies(request.Dependencies);

            // Build new indicator dependencies
            var newIndicatorDependencies = IndicatorDependencyBuilder.BuildIndicatorDependencies(indicator.IndicatorId, newDependencies);

            // Get current indicator dependencies 
            var currentIndicatorDependencies = indicator.Dependencies;

            // Update dependencies
            _mainDbContext.UpdateCollection(currentIndicatorDependencies, newIndicatorDependencies);

            // Update indicator
            indicator.Update(request.Name, request.Description, request.Formula);

            // Update
            _mainDbContext.Indicators.Update(indicator);
            
            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<Responses.Indicator>(indicator);

            // Return
            return response;
        }

        public async Task<List<Indicator>> UpdateIndicatorDependencies()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get all indicators
            var indicators = await _mainDbContext.Indicators.ToListAsync();

            // Get all indicators dependencies
            var indicatorDependencies = await _mainDbContext.IndicatorDependencies.ToListAsync();

            // Build dependency levels
            IndicatorBuilder.BuildDependencyLevels(indicators, indicatorDependencies);

            // Update
            _mainDbContext.Indicators.UpdateRange(indicators);

            // Save
            await _mainDbContext.SaveChangesAsync();

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
                var dependency = await _mainDbContext.Indicators.FindAsync(dependencyId);

                // Throw ValidationException if it does not exist
                if (dependency == null) throw new ValidationException(string.Format(IndicatorMessage.DependencyNotFound, dependencyId));

                // Add
                dependencies.Add(dependency);
            }

            // Return
            return dependencies;
        }
    }
}
