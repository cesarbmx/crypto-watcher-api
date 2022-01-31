 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
 using CesarBmx.CryptoWatcher.Application.ConflictReasons;
 using CesarBmx.Shared.Application.Exceptions;
 using CesarBmx.Shared.Common.Extensions;
 using CesarBmx.Shared.Logging.Extensions;
 using CesarBmx.Shared.Persistence.Extensions;
 using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Application.Messages;
 using CesarBmx.CryptoWatcher.Persistence.Contexts;
 using CesarBmx.Shared.Application.Responses;
 using Microsoft.EntityFrameworkCore;
 using Microsoft.Extensions.Logging;
 using Line = CesarBmx.CryptoWatcher.Domain.Models.Line;
 using Watcher = CesarBmx.CryptoWatcher.Domain.Models.Watcher;

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

        public async Task<List<Responses.Watcher>> GetUserWatchers(string userId = null, string currencyId = null, string indicatorId = null)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get user watchers
            var userWatchers = await _mainDbContext.Watchers.Where(WatcherExpression.Filter(userId, currencyId, indicatorId)).ToListAsync();

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
            if (watcher != null) throw new ConflictException(new Conflict<AddWatcherConflictReason>(AddWatcherConflictReason.DUPLICATE, WatcherMessage.WatcherAlreadyExists));

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
            var response = _mapper.Map<Responses.Watcher>(watcher);

            // Return
            return response;
        }
        public async Task<Responses.Watcher> SetWatcher(SetWatcher request)
        {
            // Get watcher
            var watcher = await _mainDbContext.Watchers.FindAsync(request.WatcherId);

            // Throw NotFound if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Buy limit must be lower than watcher value
            if (WatcherExpression.BuyLimitMustBeLowerThanWatcherValue(request.Buy).Invoke(watcher)) throw new ConflictException(new Conflict<SetWatcherConflictReason>(SetWatcherConflictReason.BUY_LIMIT_MUST_BE_LOWER_THAN_WATCHER_VALUE, string.Format(WatcherMessage.BuyLimitMustBeLowerThanWatcherValue, watcher.Value)));

            // Sell limit must be higher than watcher value
            if (WatcherExpression.SellLimitMustBeHigherThanWatcherValue(request.Sell).Invoke(watcher)) throw new ConflictException(new Conflict<SetWatcherConflictReason>(SetWatcherConflictReason.SELL_LIMIT_MUST_BE_HIGHER_THAN_WATCHER_VALUE, string.Format(WatcherMessage.SellLimitMustBeHigherThanWatcherValue, watcher.Value)));

            // Watcher already got liquidated
            if (WatcherExpression.WatcherSold().Invoke(watcher)) throw new ConflictException(new Conflict<SetWatcherConflictReason>(SetWatcherConflictReason.WATCHER_ALREADY_LIQUIDATED, WatcherMessage.WatcherAlreadyLiquidated));

            // Watcher already bought
            if (WatcherExpression.WatcherAlreadyBought(request.Buy).Invoke(watcher)) throw new ConflictException(new Conflict<SetWatcherConflictReason>(SetWatcherConflictReason.WATCHER_ALREADY_BOUGHT, string.Format(WatcherMessage.WatcherAlreadyBought, watcher.EntryPrice)));

            // Watcher already sold
            if (WatcherExpression.WatcherAlreadySold(request.Sell).Invoke(watcher)) throw new ConflictException(new Conflict<SetWatcherConflictReason>(SetWatcherConflictReason.WATCHER_ALREADY_SOLD, string.Format(WatcherMessage.WatcherAlreadySold, watcher.EntryPrice)));
            
            // Set watcher
            watcher.Set(request.Buy, request.Sell, request.Quantity);

            // Update
            _mainDbContext.Watchers.Update(watcher);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Response
            var response = _mapper.Map<Responses.Watcher>(watcher);

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Return
            return response;
        }
        public async Task<Responses.Watcher> EnableWatcher(EnableWatcher request)
        {
            // Get watcher
            var watcher = await _mainDbContext.Watchers.FindAsync(request.WatcherId);

            // Throw NotFound if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Update watcher
            watcher.Enable(request.Enabled);

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

        public async Task<List<Watcher>> UpdateDefaultWatchers(List<Line> lines)
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
            _logger.LogSplunkInformation(nameof(UpdateDefaultWatchers), new
            {
                newDefaultWatchers.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return 
            return newDefaultWatchers;
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
    }
}
