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
                    false,
                    now);

                // Set
                watcher.Set(line.AverageBuy, line.AverageSell, null);

                // Sync
                watcher.Sync(line.AverageBuy, line.AverageSell, line.Value, line.Price);

                // Add
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
                if (defaultWatcher != null) watcher.Sync(defaultWatcher);
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
        public static Guid BuildOrderId(this Watcher watcher)
        {
            if (watcher.SellingOrder != null) return watcher.SellingOrder.OrderId;
            if (watcher.BuyingOrder != null) return watcher.BuyingOrder.OrderId;
            throw new NotImplementedException();

        }
    }
}
