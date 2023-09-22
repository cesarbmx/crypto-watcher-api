using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.Shared.Persistence.Extensions;
using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using CesarBmx.Shared.Application.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Messaging.Ordering.Commands;
using CesarBmx.Shared.Messaging.Ordering.Types;
using MassTransit;

namespace CesarBmx.CryptoWatcher.Application.Services
{
    public class WatcherService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<WatcherService> _logger;
        private readonly IMapper _mapper;
        private readonly ActivitySource _activitySource;
        private readonly IBus _bus;

        public WatcherService(
            MainDbContext mainDbContext,
            ILogger<WatcherService> logger,
            IMapper mapper,
            ActivitySource activitySource,
            IBus bus)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _mapper = mapper;
            _activitySource = activitySource;
            _bus = bus;
        }

        public async Task<List<Responses.Watcher>> GetUserWatchers(string userId = null, string currencyId = null, string indicatorId = null)
        {
            // Start span
            using var span = _activitySource.StartActivity(nameof(GetUserWatchers));

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
            // Start span
            using var span = _activitySource.StartActivity(nameof(GetWatcher));

            // Get watcher
            var watcher = await _mainDbContext.Watchers.FirstOrDefaultAsync(x => x.WatcherId == watcherId);

            // Watcher not found
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Response
            var response = _mapper.Map<Responses.Watcher>(watcher);

            // Return
            return response;
        }
        public async Task<Responses.Watcher> AddWatcher(AddWatcher request)
        {
            // Start span
            using var span = _activitySource.StartActivity(nameof(AddWatcher));

            // Get currency
            var currency = await _mainDbContext.Currencies.FindAsync(request.CurrencyId);

            // Watcher not found
            if (currency == null) throw new NotFoundException(CurrencyMessage.CurrencyNotFound);

            // Get user
            var user = await _mainDbContext.Users.FindAsync(request.UserId);

            // User not found
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get indicator
            var indicator = await _mainDbContext.Indicators.FindAsync(request.IndicatorId);

            // Indicator not found
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Get watcher
            var watcher = await _mainDbContext.Watchers.FirstOrDefaultAsync(WatcherExpression.Unique(request.UserId, request.CurrencyId, indicator.UserId, indicator.IndicatorId));

            // Watcher already exists
            if (watcher != null) throw new ConflictException(new AddWatcherConflict(AddWatcherConflictReason.WATCHER_ALREADY_EXISTS, WatcherMessage.WatcherAlreadyExists));

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

            // Response
            var response = _mapper.Map<Responses.Watcher>(watcher);

            // Log
            _logger.LogInformation("{@Event}, {@Id}, {@UserId}, {@Request}, {@Response}", "WatcherAdded", Guid.NewGuid(), request.UserId, request, response);

            // Return
            return response;
        }
        public async Task<Responses.Watcher> SetWatcher(SetWatcher request)
        {
            // Start span
            using var span = _activitySource.StartActivity(nameof(SetWatcher));

            // Get watcher
            var watcher = await _mainDbContext.Watchers.FindAsync(request.WatcherId);

            // Watcher not found
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Buy limit must be lower than watcher value
            if (WatcherExpression.BuyLimitHigherThanWatcherValue(request.Buy).Invoke(watcher)) throw new ConflictException(new SetWatcherConflict(SetWatcherConflictReason.BUY_LIMIT_MUST_BE_LOWER_THAN_WATCHER_VALUE, string.Format(WatcherMessage.BuyLimitMustBeLowerThanWatcherValue, watcher.Value)));

            // Sell limit must be higher than watcher value
            if (WatcherExpression.SellLimitLowerThanWatcherValue(request.Sell).Invoke(watcher)) throw new ConflictException(new SetWatcherConflict(SetWatcherConflictReason.SELL_LIMIT_MUST_BE_HIGHER_THAN_WATCHER_VALUE, string.Format(WatcherMessage.SellLimitMustBeHigherThanWatcherValue, watcher.Value)));

            // Watcher already bought
            if (WatcherExpression.WatcherBought().Invoke(watcher)) throw new ConflictException(new SetWatcherConflict(SetWatcherConflictReason.WATCHER_ALREADY_BOUGHT, string.Format(WatcherMessage.WatcherAlreadyBought, watcher.EntryPrice)));

            // Watcher already sold
            if (WatcherExpression.WatcherSold().Invoke(watcher)) throw new ConflictException(new SetWatcherConflict(SetWatcherConflictReason.WATCHER_ALREADY_SOLD, string.Format(WatcherMessage.WatcherAlreadySold, watcher.ExitPrice)));
            
            // Set watcher
            watcher.Set(request.Buy, request.Sell, request.Quantity);

            // Update
            _mainDbContext.Watchers.Update(watcher);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Response
            var response = _mapper.Map<Responses.Watcher>(watcher);

            // Log
            _logger.LogInformation("{@Event}, {@Id}, {@UserId}, {@Request}, {@Response}", "WatcherSet", Guid.NewGuid(), request.UserId, request, response);

            // Return
            return response;
        }
        public async Task<Responses.Watcher> EnableWatcher(EnableWatcher request)
        {
            // Start span
            using var span = _activitySource.StartActivity(nameof(EnableWatcher));

            // Get watcher
            var watcher = await _mainDbContext.Watchers.FindAsync(request.WatcherId);

            // Watcher not found
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Watcher already enabled
            if (watcher.Enabled == request.Enabled && request.Enabled) throw new ConflictException(new EnableWatcherConflict(EnableWatcherConflictReason.WATCHER_ALREADY_ENABLED, WatcherMessage.WatcherAlreadyEnabled));

            // Watcher already disabled
            if (watcher.Enabled == request.Enabled && !request.Enabled) throw new ConflictException(new EnableWatcherConflict(EnableWatcherConflictReason.WATCHER_ALREADY_DISABLED, WatcherMessage.WatcherAlreadyDisabled));

            // Update watcher
            watcher.Enable(request.Enabled);

            // Update
            _mainDbContext.Watchers.Update(watcher);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Response
            var response = _mapper.Map<Responses.Watcher>(watcher);

            if (request.Enabled)
            {
                // Log
                _logger.LogInformation("{@Event}, {@Id}, {@UserId}, {@Request}, {@Response}", "WatcherEnabled", Guid.NewGuid(), request.UserId, request, response);
            }
            else
            {
                // Log
                _logger.LogInformation("{@Event}, {@Id}, {@UserId}, {@Request}, {@Response}", "WatcherDisabled", Guid.NewGuid(), request.UserId, request, response);
            }

            // Return
            return response;
        }

        public async Task<List<Watcher>> UpdateDefaultWatchers(List<Line> lines)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Start span
            using var span = _activitySource.StartActivity(nameof(UpdateDefaultWatchers));

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

            // Log
            _logger.LogInformation("{@Event}, {@Id}, {@Count}, {@ExecutionTime}", "DefaultWatchersUpdated", Guid.NewGuid(), newDefaultWatchers.Count, stopwatch.Elapsed.TotalSeconds);

            // Return 
            return newDefaultWatchers;
        }
        public async Task<List<Watcher>> UpdateWatchers(List<Watcher> defaultWatchers)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Start span
            using var span = _activitySource.StartActivity(nameof(UpdateWatchers));

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

            // Log
            _logger.LogInformation("{@Event}, {@Id}, {@Count}, {@ExecutionTime}", "WatchersUpdated", Guid.NewGuid(), watchers.Count, stopwatch.Elapsed.TotalSeconds);

            // Return
            return watchers;
        }
        public async Task PlaceOrders(List<Watcher> watchers)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Start span
            using var span = _activitySource.StartActivity(nameof(PlaceOrders));

            // Grab watchers willing to buy or sell
            var watchersWillingToBuyOrSell = watchers.Where(WatcherExpression.WatcherBuyingOrSelling()).ToList();

            // Set as buying or selling
            watchersWillingToBuyOrSell = watchersWillingToBuyOrSell.SetAsBuyingOrSelling();

            // Build PlaceOrders
            var placeOrders = watchersWillingToBuyOrSell.BuildPlaceOrders();

            // Send place orders
            foreach(var watcher in watchersWillingToBuyOrSell)
            {
                // Send
                await _bus.Send(placeOrders);
            }          

            // Update watchers
            _mainDbContext.Watchers.UpdateRange(watchersWillingToBuyOrSell);

            // Save changes
            await _mainDbContext.SaveChangesAsync(); 

            // Stop watch
            stopwatch.Stop();

            // Log
            _logger.LogInformation("{@Event}, {@Id}, {@OrdersWillingToBuyOrSellCount}, {@ExecutionTime}", "OrdersPlaced", Guid.NewGuid(), watchersWillingToBuyOrSell.Count, stopwatch.Elapsed.TotalSeconds);

        }
    }
}
