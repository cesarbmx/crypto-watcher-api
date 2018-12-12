using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilder
    {
        public static WatcherStatus BuildStatus(decimal indicatorValue, decimal buy, decimal sell)
        {
            // Evaluate
            var watcherStatus = WatcherStatus.Hold;
            if(indicatorValue >= buy) watcherStatus = WatcherStatus.Buy;
            if (indicatorValue <= sell) watcherStatus = WatcherStatus.Sell;

            // Return
            return watcherStatus;
        }
        public static List<Watcher> BuildWatchersWithDefaults(List<Watcher> watchers, List<Watcher> defaultWatchers)
        {
            var watchersWithDefaults = new List<Watcher>();
            foreach (var defaultWatcher in defaultWatchers)
            {
                // Get matching watcher
                var watcher = watchers.FirstOrDefault(x =>
                    x.IndicatorId == defaultWatcher.IndicatorId &&
                    x.CurrencyId == defaultWatcher.CurrencyId);

                // If the watcher does not exist, we use the default one
                if (watcher == null) watcher = defaultWatcher;

                // Add
                watchersWithDefaults.Add(watcher);
            }

            return watchersWithDefaults;
        }
        public static List<Watcher> BuildDefaultWatchers(List<Line> lines)
        {
            var watchers = new List<Watcher>();
            foreach (var line in lines)
            {
                var lineAverageBuy = lines.FirstOrDefault(x => x.CurrencyId == line.CurrencyId && x.IndicatorId == "master-average-buy");
                var lineAverageSell = lines.FirstOrDefault(x => x.CurrencyId == line.CurrencyId && x.IndicatorId == "master-average-sell");
                var averageBuy = lineAverageBuy?.Value ?? 0m;
                var averageSell = lineAverageSell?.Value ?? 0m;

                // Add default watcher
                var watcher = new Watcher(
                        "master",
                        line.CurrencyId,
                        line.IndicatorId,
                        line.Value,
                        averageBuy,
                        averageSell,
                        averageBuy,
                        averageSell,
                        false);
                    watchers.Add(watcher);
            }

            // Return
            return watchers;
        }
        public static List<Watcher> SyncWatchers(this List<Watcher> watchers, List<Watcher> defaultWatchers)
        {
            // Sync watcher
            foreach (var watcher in watchers)
            {
                var currencyId = watcher.CurrencyId;
                var indicatorId = watcher.IndicatorId;
                var defaultWatcher = defaultWatchers.FirstOrDefault(WatcherExpression.Watcher("master", currencyId, indicatorId).Compile());
                if (defaultWatcher != null) watcher.Sync(defaultWatcher.Value, defaultWatcher.AverageBuy, defaultWatcher.AverageSell);
            }

            // Return
            return watchers;
        }
    }
}
