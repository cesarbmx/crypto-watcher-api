using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Domain.Services
{
    public class WatcherService
    {
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly UserService _userService;
        private readonly CurrencyService _currencyService;
        private readonly ILogger<WatcherService> _logger;

        public WatcherService(
            IRepository<Watcher> watcherRepository,
            UserService userService,
            CurrencyService currencyService,
            ILogger<WatcherService> logger)
        {
            _watcherRepository = watcherRepository;
            _userService = userService;
            _currencyService = currencyService;
            _logger = logger;
        }

        public async Task<List<Watcher>> GetUserWatchers(string userId)
        {
            // Get user watchers
            var userWatchers = new List<Watcher>();
            userWatchers.AddRange(await GetUserWatchersByIndicatorType(userId, IndicatorType.PriceChange));
            userWatchers.AddRange(await GetUserWatchersByIndicatorType(userId, IndicatorType.Hype));

            // Return
            return userWatchers;
        }
        public async Task<List<Watcher>> GetUserWatchersByIndicatorType(string userId, IndicatorType indicator)
        {
            // Get user
            var user = await _userService.GetUser(userId);

            // Throw NotFound exception if it does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get user watchers
            var userWatchers = await _watcherRepository.Get(WatcherExpression.UserWatcher(user.Id));

            // Get currencies
            var currencies = await _currencyService.GetCurrencies();

            // For each currency
            var watchers = new List<Watcher>();
            foreach (var currency in currencies)
            {
                // If the watcher exists, we add it
                var watcher = userWatchers.FirstOrDefault(x =>
                    x.IndicatorType == indicator && x.Id == currency.Id);

                // If the watcher does not exist, we add a default one
                if (watcher == null)
                {
                    watcher = new Watcher(
                        user.Id,
                        currency.Id,
                        indicator,
                        IndicatorBuilder.BuildValue(currency, indicator, currencies),
                        new WatcherSettings(5, 5),
                        new WatcherSettings(0, 0),
                        false);
                }

                watchers.Add(watcher);
            }

            // Return
            return watchers;
        }
        public async Task<List<Watcher>> GetWatchersWillingToBuy()
        {
            // Get watchers
            var watchers = await _watcherRepository.GetAll();

            // Select those willing to buy
            watchers = watchers.Where(x => x.Status == WatcherStatus.Buy).ToList();

            // Return
            return watchers;
        }
        public async Task<List<Watcher>> GetWatchersWillingToSell()
        {
            // Get watchers
            var watchers = await _watcherRepository.GetAll();

            // Select those willing to sell
            watchers = watchers.Where(x => x.Status == WatcherStatus.Sell).ToList();

            // Return
            return watchers;
        }
        public async Task<Watcher> GetWatcher(string id)
        {
            // Get watcher
            var watcher = await _watcherRepository.GetById(id);

            // Throw NotFound exception if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Return
            return watcher;
        }
        public async Task<Watcher> AddWatcher(string userId, IndicatorType indicator, string id, WatcherSettings settings)
        {
            // Get user
            var user = await _userService.GetUser(userId);

            // Get currencies
            var currency = await _currencyService.GetCurrency(id);

            // Get currencies
            var currencies = await _currencyService.GetCurrencies();

            // Add watcher
            var watcher = new Watcher(
                user.Id,
                currency.Id,
                indicator,
                IndicatorBuilder.BuildValue(currency, indicator, currencies),
                settings,
                new WatcherSettings(0,0),
                false);
            _watcherRepository.Add(watcher);

            // Log into Splunk
            _logger.LogSplunkInformation(nameof(LoggingEvents.WatcherAdded), watcher);

            // Return
            return watcher;
        }
        public async Task<Watcher> UpdateWatcherSettings(string id, decimal buyAt, decimal sellAt)
        {
            // Get watcher
            var watcher = await GetWatcher(id);

            // Update settings
            var settings = new WatcherSettings(buyAt,sellAt);
            watcher.UpdateSettings(settings);

            // Return
            return watcher;
        }
    }
}
