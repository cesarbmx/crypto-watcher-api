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
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Domain.Models;
 using CesarBmx.CryptoWatcher.Persistence.Contexts;
 using Microsoft.EntityFrameworkCore;
 using Microsoft.Extensions.Logging;

namespace CesarBmx.CryptoWatcher.Application.Services
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

        public async Task<List<Resources.Watcher>> GetUserWatchers(string userId = null, string currencyId = null, string indicatorId = null)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get user watchers
            var userWatchers = await _mainDbContext.Watchers.Where(WatcherExpression.Filter(userId, currencyId, indicatorId)).ToListAsync();

            // Response
            var response = _mapper.Map<List<Resources.Watcher>>(userWatchers);

            // Return
            return response;
        }
        public async Task<Resources.Watcher> GetWatcher(int watcherId)
        {
            // Get watcher
            var watcher = await _mainDbContext.Watchers.FirstOrDefaultAsync(x=>x.WatcherId == watcherId);

            // Throw NotFound if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Response
            var response = _mapper.Map<Resources.Watcher>(watcher);

            // Return
            return response;
        }
        public async Task<Resources.Watcher> AddWatcher(AddWatcher request)
        {
            // Get currency
            var currency = await _mainDbContext.Currencies.FindAsync(request.CurrencyId);

            // Throw NotFound if the currency does not exist
            if (currency == null) throw new NotFoundException(CurrencyMessage.CurrencyNotFound);

            // Get user
            var user = await _mainDbContext.Users.FindAsync(request.UserId);

            // Throw NotFound if it does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get indicator
            var indicator = await _mainDbContext.Indicators.FindAsync(  request.IndicatorId);

            // Throw NotFound if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Check if it exists
            var watcher = await _mainDbContext.Watchers.FirstOrDefaultAsync(WatcherExpression.Unique(request.UserId, request.CurrencyId, indicator.UserId, indicator.IndicatorId));

            // Throw ConflictException if it exists
            if (watcher != null) throw new ConflictException(WatcherMessage.WatcherAlreadyExists);

            // Get default watcher
            var defaultWatcher = await _mainDbContext.Watchers.FirstOrDefaultAsync(WatcherExpression.DefaultWatcher(request.CurrencyId, request.IndicatorId));
            
            // Add watcher
            watcher = new Watcher(
                request.UserId,
                request.CurrencyId,
                indicator.IndicatorId,
                defaultWatcher?.Value,
                null,
                null,
                null,
                defaultWatcher?.AverageBuy,
                defaultWatcher?.AverageSell,
                defaultWatcher?.Price,
                request.Enabled,
                DateTime.UtcNow.StripSeconds());

            // Add
            _mainDbContext.Watchers.Add(watcher);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<Resources.Watcher>(watcher);

            // Return
            return response;
        }
        public async Task<Resources.Watcher> UpdateWatcher(UpdateWatcher request)
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
            var response = _mapper.Map<Resources.Watcher>(watcher);

            // Return
            return response;
        }
        public async Task<List<Watcher>> UpdateWatchers(List<Watcher> defaultWatchers)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get all watchers
            var watchers = await _mainDbContext.Watchers.Where(WatcherExpression.NonDefaultWatcher()).ToListAsync();

            // Sync watchers
            watchers.SyncWatchers(defaultWatchers);

            // Update
            _mainDbContext.Watchers.UpdateRange(watchers);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation(nameof(UpdateWatchers), new
            {
                watchers.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return
            return watchers;
        }

        public async Task<List<Watcher>> SetDefaultWatchers(List<Line> lines)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Build default watchers
            var newDefaultWatchers = WatcherBuilder.BuildDefaultWatchers(lines);

            // Get all default watchers
            var defaultWatchers = await _mainDbContext.Watchers.Where(WatcherExpression.DefaultWatcher()).ToListAsync();

            // Update 
            _mainDbContext.UpdateCollection(defaultWatchers, newDefaultWatchers);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation(nameof(SetDefaultWatchers), new
            {
                newDefaultWatchers.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return 
            return newDefaultWatchers;
        }
    }
}
