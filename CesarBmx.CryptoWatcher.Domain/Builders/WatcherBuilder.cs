using System;
using System.Collections.Generic;
using System.Linq;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Domain.Types;


namespace CesarBmx.CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilder
    {
        public static WatcherStatus BuildStatus(Watcher watcher)
        {
            // Evaluate and return
            if (WatcherExpression.WatcherNotSet().Invoke(watcher)) return WatcherStatus.NOT_SET;
            if (WatcherExpression.WatcherBuying().Invoke(watcher)) return WatcherStatus.BUYING;
            if (WatcherExpression.WatcherBought().Invoke(watcher)) return WatcherStatus.BOUGHT;
            if (WatcherExpression.WatcherHolding().Invoke(watcher)) return WatcherStatus.HOLDING;
            if (WatcherExpression.WatcherSelling().Invoke(watcher)) return WatcherStatus.SELLING;
            if (WatcherExpression.WatcherSold().Invoke(watcher)) return WatcherStatus.SOLD;
            throw new NotImplementedException();
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
                    line.IndicatorId,
                    line.Value,
                    line.AverageBuy,
                    line.AverageSell,
                    null,
                    line.AverageBuy,
                    line.AverageSell,
                    line.Price,
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
                var defaultWatcher = defaultWatchers.FirstOrDefault(WatcherExpression.DefaultWatcher(watcher.CurrencyId, watcher.IndicatorId).Compile());
                if (defaultWatcher != null) watcher.Sync(defaultWatcher.Value, defaultWatcher.AverageBuy, defaultWatcher.AverageSell, defaultWatcher.Price);
            }
        }
        public static decimal? BuildProfit(decimal? entryPrice, decimal? exitPrice, decimal? quantity)
        {
            // Make the calculation only when watcher has exited
            if (!entryPrice.HasValue || !exitPrice.HasValue || !quantity.HasValue) return null;

            // Result
            var result = (exitPrice - entryPrice) * quantity;

            // Return
            return result;
        }
    }
}
