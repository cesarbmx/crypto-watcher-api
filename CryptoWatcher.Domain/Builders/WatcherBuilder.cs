using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilder
    {
        public static WatcherStatus BuildStatus(decimal indicatorValue, BuySell buySell)
        {
            // Evaluate
            var watcherStatus = (indicatorValue >= buySell.BuyAt) ? WatcherStatus.Buy : WatcherStatus.Sell;

            // Return
            return watcherStatus;
        }
        public static List<Watcher> BuildUserWatchersWithDefaults(this List<Watcher> userWatchers, string userId, List<Currency> currencies, List<Indicator> indicators)
        {
            var watchers = new List<Watcher>();
            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    // Get matching watcher
                    var priceChangeWatcher = userWatchers.FirstOrDefault(x =>
                        x.IndicatorId == indicator.Id &&
                        x.CurrencyId == currency.Id);

                    // If the watcher does not exist, we add the default one
                    if (priceChangeWatcher == null)
                    {
                        priceChangeWatcher = new Watcher(
                            userId,
                            currency.Id,
                            indicator.Id,
                            IndicatorBuilder.BuildValue(currency, indicator.Id, currencies),
                            new BuySell(5, 5),
                            new BuySell(0, 0),
                            false);
                    }

                    // Add
                    watchers.Add(priceChangeWatcher);
                }
            }

            return watchers;
        }
        public static List<Watcher> BuildDefaultWatchers(List<Currency> currencies, List<Indicator> indicators)
        {
            var watchers = new List<Watcher>();
            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    // Add price change watcher
                    var priceChangeWatcher = new Watcher(
                        "master",
                        currency.Id,
                        indicator.Id,
                        IndicatorBuilder.BuildValue(currency, indicator.Id, currencies),
                        new BuySell(5, 5),
                        new BuySell(0, 0),
                        false);
                    watchers.Add(priceChangeWatcher);

                    // Add hyper watcher
                    var hypeWatcher = new Watcher(
                        "master",
                        currency.Id,
                        indicator.Id,
                        IndicatorBuilder.BuildValue(currency, indicator.Id, currencies),
                        new BuySell(5, 5),
                        new BuySell(0, 0),
                        false);
                    watchers.Add(hypeWatcher);
                }
            }

            return watchers;
        }
    }
}
