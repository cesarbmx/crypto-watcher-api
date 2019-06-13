using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
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

        public async Task<List<IndicatorResponse>> GetAllIndicators(string userId, IndicatorType indicatorType)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all indicators
            var indicators = await _mainDbContext.Indicators
                .Include(x => x.Dependencies)
                .Where(IndicatorExpression.IndicatorFilter(userId, indicatorType))
                .ToListAsync();

            // Response
            var response = _mapper.Map<List<IndicatorResponse>>(indicators);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> GetIndicator(string indicatorId)
        {
            // Get indicator
            var indicator = await _mainDbContext.Indicators
                .Include(x => x.Dependencies)
                .SingleOrDefaultAsync(IndicatorExpression.Indicator(indicatorId));

            // Throw NotFoundException if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> AddIndicator(AddIndicatorRequest request)
        {
            // Get indicator
            var indicator = await _mainDbContext.Indicators.SingleOrDefaultAsync(IndicatorExpression.Indicator(request.IndicatorId));

            // Throw ConflictException if it exists
            if (indicator != null) throw new ConflictException(IndicatorMessage.IndicatorWithSameIdAlreadyExists);

            // Time
            var time = DateTime.Now;

            // Get dependencies
            var dependencies = await GetDependencies(request.Dependencies);

            // Build indicator dependencies
            var indicatorDependencies = IndicatorDependencyBuilder.BuildIndicatorDependencies(request.IndicatorId, dependencies, time);

            // Build dependency level
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(dependencies);

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
                time);

            // Add
            _mainDbContext.Indicators.Add(indicator);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkRequest(request);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> UpdateIndicator(UpdateIndicatorRequest request)
        {
            // Get indicator
            var indicator = await _mainDbContext.Indicators
                .Include(x=>x.Dependencies)
                .SingleOrDefaultAsync(IndicatorExpression.Indicator(request.IndicatorId));

            // Throw NotFoundException if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Time
            var time = DateTime.Now;

            // Get dependencies
            var newDependencies = await GetDependencies(request.Dependencies);

            // Build new indicator dependencies
            var newIndicatorDependencies = IndicatorDependencyBuilder.BuildIndicatorDependencies(indicator.IndicatorId, newDependencies, time);

            // Build dependency level
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(newDependencies);

            // Update indicator
            indicator.Update(request.Name, request.Description, request.Formula, newIndicatorDependencies, dependencyLevel);

            // Update
            _mainDbContext.Indicators.Update(indicator);         

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkRequest(request);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }

        private async Task<List<Indicator>> GetDependencies(string[] dependencyIds)
        {
            var dependencies = new List<Indicator>();
            foreach (var dependencyId in dependencyIds)
            {
                // Get indicator
                var dependency = await _mainDbContext.Indicators.FindAsync(dependencyId);

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
