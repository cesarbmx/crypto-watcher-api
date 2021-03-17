using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.Shared.Logging.Extensions;
using CesarBmx.Shared.Persistence.Extensions;
using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CesarBmx.CryptoWatcher.Application.Services
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

        public async Task<List<Responses.Indicator>> GetAllUserIndicators(string userId)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all indicators
            var indicators = await _mainDbContext.Indicators
                .Include(x=>x.Dependencies)
                .Where(IndicatorExpression.Filter(null, userId)).ToListAsync();

            // Response
            var response = _mapper.Map<List<Responses.Indicator>>(indicators);

            // Return
            return response;
        }
        public async Task<Responses.Indicator> GetUserIndicator(string userId, string indicatorId)
        {
            // Get indicator
            var indicator = await _mainDbContext.Indicators
                .Include(x => x.Dependencies)
                .FirstOrDefaultAsync(x=> x.UserId == userId && x.IndicatorId == indicatorId);

            // Throw NotFound if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Response
            var response = _mapper.Map<Responses.Indicator>(indicator);

            // Return
            return response;
        }
        public async Task<Responses.Indicator> AddUserIndicator(AddIndicator request)
        {
            // Get indicator
            var indicator = await _mainDbContext.Indicators
                .Include(x => x.Dependencies)
                .FirstOrDefaultAsync(x => x.IndicatorId == request.IndicatorId);

            // Throw ConflictException if it exists
            if (indicator != null) throw new ConflictException(IndicatorMessage.IndicatorWithSameIdAlreadyExists);

            // Get dependencies
            var dependencies = await GetIndicators(request.Dependencies);

            // Build dependency level
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(dependencies);

            // Build new indicator dependencies
            var indicatorDependencies = IndicatorDependencyBuilder.BuildIndicatorDependencies(request.IndicatorId, dependencies);

            // Create
            indicator = new Indicator(
                request.UserId,
                request.IndicatorId,
                request.Name,
                request.Description,
                request.Formula,
                indicatorDependencies,
                dependencyLevel,
                DateTime.UtcNow.StripSeconds());

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
        public async Task<Responses.Indicator> UpdateUserIndicator(UpdateIndicator request)
        {
            // Get indicator
            var indicator = await _mainDbContext.Indicators
                .Include(x => x.Dependencies)
                .FirstOrDefaultAsync(x => x.IndicatorId == request.IndicatorId);

            // Throw NotFound if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Get dependencies
            var newDependencies = await GetIndicators(request.Dependencies);

            // Build new indicator dependencies
            var newIndicatorDependencies = IndicatorDependencyBuilder.BuildIndicatorDependencies( indicator.IndicatorId, newDependencies);

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

        public async Task<List<Indicator>> GetIndicators(List<string> indicatorIds)
        {
            var indicators = new List<Indicator>();
            foreach (var indicatorId in indicatorIds)
            {
                // Get indicator
                var indicator = await _mainDbContext.Indicators.FirstOrDefaultAsync(x=>x.IndicatorId == indicatorId);

                // Throw NotFound if it does not exist
                if (indicator == null) throw new NotFoundException(string.Format(IndicatorDependencyMessage.IndicatorDependencyNotFound, indicatorId));

                // Add
                indicators.Add(indicator);
            }

            // Return
            return indicators;
        }
        public async Task<List<Indicator>> UpdateDependencyLevels()
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
            _logger.LogSplunkInformation(nameof(UpdateDependencyLevels), new
            {
                MaxLevel = maxDependencyLevel,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return
            return indicators;
        }
    }
}
