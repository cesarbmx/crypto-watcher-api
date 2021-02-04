using System;
using System.Collections.Generic;
using System.Linq;
using CesarBmx.Shared.Common.Extensions;
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
            var watcherStatus = WatcherStatus.HOLD;
            if(value >= buy) watcherStatus = WatcherStatus.BUY;
            if (value <= sell) watcherStatus = WatcherStatus.SELL;

            // Return
            return watcherStatus;
        }
        public static List<Watcher> BuildDefaultWatchers(List<Line> lines)
        {
            var now = DateTime.UtcNow.StripSeconds();
            var watchers = new List<Watcher>();
            foreach (var line in lines)
            {
                // Add default watcher
                var watcher = new Watcher(
                    "master",
                    line.CurrencyId,
                    line.UserId,
                    line.IndicatorId,
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
                var defaultWatcher = defaultWatchers.FirstOrDefault(WatcherExpression.DefaultWatcher(watcher.CurrencyId, watcher.CreatorId, watcher.IndicatorId).Compile());
                if (defaultWatcher != null) watcher.Sync(defaultWatcher.Value, defaultWatcher.AverageBuy, defaultWatcher.AverageSell);
            }
        }
    }
}
