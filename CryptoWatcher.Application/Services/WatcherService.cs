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
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
 using CryptoWatcher.Persistence.Contexts;
 using Microsoft.EntityFrameworkCore;
 using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class WatcherService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<WatcherService> _logger;
        private readonly IMapper _mapper;

        public WatcherService(
            MainDbContext mainDbContext,
            ILogger<WatcherService> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Responses.Watcher>> GetUserWatchers(string userId = null, string currencyId = null, string indicatorId = null)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Filter user watchers
            var userWatchers = await _mainDbContext.Watchers.Where(WatcherExpression.Filter(userId, currencyId, indicatorId)).ToListAsync();

            // Get all default watchers
            var defaultWatchers = await _mainDbContext.Watchers.Where(WatcherExpression.DefaultWatcher(currencyId, indicatorId)).ToListAsync();

            // Build with defaults
            userWatchers = WatcherBuilder.BuildWatchersWithDefaults(userWatchers, defaultWatchers);

            // Response
            var response = _mapper.Map<List<Responses.Watcher>>(userWatchers);

            // Return
            return response;
        }
        public async Task<Responses.Watcher> GetWatcher(int watcherId)
        {
            // Get watcher
            var watcher = await _mainDbContext.Watchers.FirstOrDefaultAsync(x=>x.WatcherId == watcherId);

            // Throw NotFound if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Response
            var response = _mapper.Map<Responses.Watcher>(watcher);

            // Return
            return response;
        }
        public async Task<Responses.Watcher> AddWatcher(AddWatcher request)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(request.UserId);

            // Throw NotFound if the currency does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get indicator
            var indicator = await _mainDbContext.Indicators.FindAsync(request.IndicatorId);

            // Throw NotFound if the currency does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Check if it exists
            var watcher = await _mainDbContext.Watchers.FirstOrDefaultAsync(WatcherExpression.Unique(request.UserId, request.CurrencyId, request.IndicatorId));

            // Throw ConflictException if it exists
            if (watcher != null) throw new ConflictException(WatcherMessage.WatcherAlreadyExists);

            // Get default watcher
            var defaultWatcher = await _mainDbContext.Watchers.FirstOrDefaultAsync(WatcherExpression.DefaultWatcher(request.CurrencyId, request.IndicatorId));

            // Add
            watcher = new Watcher(
                request.UserId,
                request.CurrencyId,
                request.IndicatorId,
                defaultWatcher?.Value,
                request.Buy,
                request.Sell,
                defaultWatcher?.AverageBuy,
                defaultWatcher?.AverageSell,
                request.Enabled,
                DateTime.Now);
            _mainDbContext.Watchers.Add(watcher);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<Responses.Watcher>(watcher);

            // Return
            return response;
        }
        public async Task<Responses.Watcher> UpdateWatcher(UpdateWatcher request)
        {
            // Get watcher
            var watcher = await _mainDbContext.Watchers.FindAsync(request.WatcherId);

            // Throw NotFound if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Update watcher
            watcher.Update(request.Buy, request.Sell, request.Enabled);

            // Update
            _mainDbContext.Watchers.Update(watcher);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<Responses.Watcher>(watcher);

            // Return
            return response;
        }
        public async Task<List<Watcher>> UpdateWatchers(List<Watcher> defaultWatchers)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get all watchers
            var watchers = await _mainDbContext.Watchers.ToListAsync();

            // Sync watchers
            watchers.SyncWatchers(defaultWatchers);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("UpdateWatchers", new
            {
                watchers.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return
            return watchers;
        }

        public async Task<List<Watcher>> UpdateDefaultWatchers(List<Line> lines)
        {
            // Return if there are no lines
            if (lines.Count == 0) return new List<Watcher>();

            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get newest time
            var newestTime = lines.Max(x => x.Time);

            // Get current lines
            var currentLines = await _mainDbContext.Lines.Where(LineExpression.CurrentLine(newestTime)).ToListAsync();

            // Build default watchers
            var newDefaultWatchers = WatcherBuilder.BuildDefaultWatchers(currentLines);

            // Get all default watchers
            var defaultWatchers = await _mainDbContext.Watchers.Where(WatcherExpression.DefaultWatcher()).ToListAsync();

            // Update 
            _mainDbContext.UpdateCollection(defaultWatchers, newDefaultWatchers);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("UpdateDefaultWatchers", new
            {
                newDefaultWatchers.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return 
            return defaultWatchers;
        }
    }
}
