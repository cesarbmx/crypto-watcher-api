using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Domain.Services
{
    public class WatcherService
    {
        private readonly IWatcherRepository _watcherRepository;
        private readonly UserService _userService;
        private readonly CurrencyService _currencyService;

        public WatcherService(
            IWatcherRepository watcherRepository,
            UserService userService,
            CurrencyService currencyService)
        {
            _watcherRepository = watcherRepository;
            _userService = userService;
            _currencyService = currencyService;
        }

        public async Task<List<Watcher>> GetWatchers(string userId)
        {
            // Get user watchers
            var userWatchers = new List<Watcher>();
            userWatchers.AddRange(await GetWatchers(userId, Indicator.PriceChange));
            userWatchers.AddRange(await GetWatchers(userId, Indicator.Hype));

            // Return
            return userWatchers;
        }
        public async Task<List<Watcher>> GetWatchers(string userId, Indicator indicator)
        {
            // Get user
            var user = await _userService.GetUser(userId);

            // Get user watchers
            var userWatchers = await _watcherRepository.GetByUserId(userId);

            // Get currencies
            var currencies = await _currencyService.GetCurrencies();

            // For each currency
            var watchers = new List<Watcher>();
            foreach (var currency in currencies)
            {
                // If the watcher exists, we add it
                var watcher = userWatchers.FirstOrDefault(x =>
                    x.IndicatorId == indicator && x.CurrencyId == currency.CurrencyId);

                // If the watcher does not exist, we add a default one
                if (watcher == null)
                {
                    watcher = new Watcher(
                        user.UserId,
                        indicator,
                        currency.CurrencyId,
                        IndicatorBuilder.BuildIndicatorValue(currency, indicator, currencies),
                        new WatcherSettings(5, 5),
                        new WatcherSettings(0, 0),
                        false);
                }

                watchers.Add(watcher);
            }

            // Return
            return watchers;
        }
        public async Task<List<Watcher>> GetWatchersReadyToBuy()
        {
            // Get watchers
            var watchers = await _watcherRepository.GetBuysWithoutOrder();

            // Return
            return watchers;
        }
        public async Task<List<Watcher>> GetWatchersReadyToSell()
        {
            // Get watchers
            var watchers = await _watcherRepository.GetSellsWithOrder();

            // Return
            return watchers;
        }
        public async Task<Watcher> GetWatcher(string watcherId)
        {
            // Get watcher
            var watcher = await _watcherRepository.GetByWatcherId(watcherId);

            // Throw NotFound exception if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessages.WatcherNotFound);

            // Return
            return watcher;
        }
        public async Task<Watcher> AddWatcher(string userId, Indicator indicator, string currencyId)
        {
            // Get user
            var user = await _userService.GetUser(userId);

            // Get currencies
            var currency = await _currencyService.GetCurrency(currencyId);

            // Get currencies
            var currencies = await _currencyService.GetCurrencies();

            // Add watcher
            var watcher = new Watcher(
                user.UserId,
                indicator,
                currency.CurrencyId,
                IndicatorBuilder.BuildIndicatorValue(currency, indicator, currencies),
                new WatcherSettings(5,5),
                new WatcherSettings(0,0),
                false);
            _watcherRepository.Add(watcher);

            return watcher;
        }
        public async Task<Watcher> UpdateWatcherSettings(string watcherId, decimal buyAt, decimal sellAt)
        {
            // Get watcher
            var watcher = await GetWatcher(watcherId);

            // Update settings
            var settings = new WatcherSettings(buyAt,sellAt);
            watcher.UpdateSettings(settings);

            // Return
            return watcher;
        }
    }
}
