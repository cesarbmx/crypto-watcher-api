using System.Collections.Generic;
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
            // Get user
            var user = await _userService.GetUser(userId);

            // Get user watchers
            var userWatchers = await _watcherRepository.GetByUserId(userId);

            // Get currencies
            var currencies = await _currencyService.GetCurrencies();

            // Collect percentages
            var percentages = new decimal[currencies.Count];
            for (var i = 0; i < currencies.Count; i++)
            {
                percentages[i] = currencies[i].CurrencyPercentageChange24H;
            }

            // Add price change watcher
            foreach (var currency in currencies)
            {
                // Price watcher
                var priceChangeWatcher = new Watcher(
                    user.UserId,
                    WatcherType.PriceChange,
                    currency.CurrencyId,
                    currency.CurrencyPercentageChange24H,
                    new WatcherSettings(5, 5),
                    new WatcherSettings(0, 0),
                    false);
                userWatchers.Add(priceChangeWatcher);
            }

            // Calculate all hypes
            WatcherBuilders.BuildHypes(percentages);

            // Add hype watcher
            var index = 0;
            foreach (var currency in currencies)
            {
                // Hype watcher
                var hypeWatcher = new Watcher(
                    userId,
                    WatcherType.Hype,
                    currency.CurrencyId,
                    percentages[index],
                    new WatcherSettings(5, 5),
                    new WatcherSettings(0, 0),
                    false);
                userWatchers.Add(hypeWatcher);
                index++;
            }

            // Return
            return userWatchers;
        }
        public async Task<List<Watcher>> GetWatchers(string userId, WatcherType watcherType)
        {
            // Get user
            var user = await _userService.GetUser(userId);

            // Get user watchers
            var userWatchers = await _watcherRepository.GetByUserId(userId);

            // Get currencies
            var currencies = await _currencyService.GetCurrencies();

            // Collect percentages
            var percentages = new decimal[currencies.Count];
            for (var i = 0; i < currencies.Count; i++)
            {
                percentages[i] = currencies[i].CurrencyPercentageChange24H;
            }

            // Calculate WatcherValues
            WatcherBuilders.BuildWatcherValues(watcherType, percentages);

            // For each currency
            var index = 0;
            foreach (var currency in currencies)
            {
                // Add watcher
                var watcher = new Watcher(
                    user.UserId,
                    watcherType,
                    currency.CurrencyId,
                    percentages[index],
                    new WatcherSettings(5, 5),
                    new WatcherSettings(0, 0),
                    false);
                userWatchers.Add(watcher);
                index++;
            }

            // Return
            return userWatchers;
        }
        public async Task<Watcher> GetWatcher(string watcherId)
        {
            // Get watcher by id
            var watcher = await _watcherRepository.GetByWatcherId(watcherId);

            // Throw NotFound exception if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessages.WatcherNotFound);

            // Return
            return watcher;
        }
        public async Task<Watcher> AddWatcher(string userId, WatcherType watcherType, string currencyId)
        {
            // Get user
            var user = await _userService.GetUser(userId);

            // Get currencies
            var currency = await _currencyService.GetCurrency(currencyId);

            // Get currencies
            var currencies = await _currencyService.GetCurrencies();

            // Collect percentages
            var percentages = new decimal[currencies.Count];
            var find = 0;
            for (var i = 0; i < currencies.Count; i++)
            {
                if (currencies[i].CurrencyId == currency.CurrencyId) find = i;
                percentages[i] = currencies[i].CurrencyPercentageChange24H;
            }

            // Calculate WatcherValues
            WatcherBuilders.BuildWatcherValues(watcherType, percentages);

            // Add watcher
            var watcher = new Watcher(
                user.UserId,
                watcherType,
                currency.CurrencyId,
                percentages[find],
                new WatcherSettings(5,5),
                new WatcherSettings(0,0),
                false);
            _watcherRepository.Add(watcher);

            return watcher;
        }
        public async Task<Watcher> UpdateWatcherSettings(string watcherId, decimal buyAt, decimal sellAt)
        {
            // Get watcher by id
            var watcher = await _watcherRepository.GetByWatcherId(watcherId);

            // Throw NotFound exception if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessages.WatcherNotFound);

            // Update settings
            var settings = new WatcherSettings(buyAt,sellAt);
            watcher.UpdateSettings(settings);

            // Return
            return watcher;
        }
    }
}
