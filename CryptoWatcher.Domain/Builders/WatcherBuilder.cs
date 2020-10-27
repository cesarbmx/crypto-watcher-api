using System;
using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilder
    {
        public static WatcherStatus BuildStatus(decimal? value, decimal? buy, decimal? sell)
        {
            // Evaluate
            var watcherStatus = WatcherStatus.Hold;
            if(value >= buy) watcherStatus = WatcherStatus.Buy;
            if (value <= sell) watcherStatus = WatcherStatus.Sell;

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
            var now = DateTime.Now;
            var watchers = new List<Watcher>();
            foreach (var line in lines)
            {
                // Add default watcher
                var watcher = new Watcher(
                        "master",
                        line.CurrencyId,
                        line.IndicatorId,
                        line.IndicatorType,
                        line.Value,
                        line.AverageBuy,
                        line.AverageSell,
                        line.AverageBuy,
                        line.AverageSell,
                        false,
                        now);
                    watchers.Add(watcher);
            }

            // Return
            return watchers;
        }
        public static void SyncWatchers(this List<Watcher> watchers, List<Watcher> defaultWatchers)
        {
            // Sync watcher
            foreach (var watcher in watchers)
            {
                // We skip default watchers
                if (watcher.UserId == "master") continue;

                var currencyId = watcher.CurrencyId;
                var indicatorId = watcher.IndicatorId;
                var defaultWatcher = defaultWatchers.FirstOrDefault(WatcherExpression.DefaultWatcher(currencyId, indicatorId).Compile());
                if (defaultWatcher != null) watcher.Sync(defaultWatcher.Value, defaultWatcher.AverageBuy, defaultWatcher.AverageSell);
            }
        }
    }
}
