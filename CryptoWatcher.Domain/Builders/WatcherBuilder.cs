using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilder
    {
        public static WatcherStatus BuildStatus(decimal indicatorValue, decimal buy, decimal sell)
        {
            // Evaluate
            var watcherStatus = (indicatorValue >= buy) ? WatcherStatus.Buy : WatcherStatus.Sell;

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
                    var watcher = userWatchers.FirstOrDefault(x =>
                        x.IndicatorId == indicator.Id &&
                        x.CurrencyId == currency.Id);

                    // If the watcher does not exist, we add the default one
                    if (watcher == null)
                    {
                        watcher = new Watcher(
                            userId,
                            currency.Id,
                            indicator.Id,
                            IndicatorBuilder.BuildValue(currency, indicator.Id, currencies),
                            5,5,
                            0,0,
                            false);
                    }

                    // Add
                    watchers.Add(watcher);
                }
            }

            return watchers;
        }
        public static List<Watcher> BuildDefaultWatchers(List<Line> lines)
        {
            var watchers = new List<Watcher>();
            foreach (var line in lines)
            {
                    // Add default watcher
                    var watcher = new Watcher(
                        "master",
                        line.CurrencyId,
                        line.IndicatorId,
                        line.Value,
                        line.RecommendedBuy,
                        line.RecommendedSell,
                        line.RecommendedBuy,
                        line.RecommendedSell,
                        false);
                    watchers.Add(watcher);
            }

            return watchers;
        }
    }
}
