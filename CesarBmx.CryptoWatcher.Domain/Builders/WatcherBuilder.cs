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
        public static WatcherStatus BuildWatcherStatus(WatcherStatus currentStatus, decimal? buy, decimal? sell, decimal? value, bool hasBuyingOrder, bool hasSellingOrder, bool isBuyingOrderConfirmed, bool isSellingOrderConfirmed)
        {
            // If the value has not been set yet, we don't change the status
            if (!value.HasValue)
                return currentStatus;

            // If the buy has no value, it is not set
            if (currentStatus == WatcherStatus.NOT_SET && !buy.HasValue)
                return WatcherStatus.NOT_SET;

            // If the buy has value and there is no buying order, then it is set
            if (currentStatus == WatcherStatus.NOT_SET && buy.HasValue && buy < value)
                return WatcherStatus.SET;

            // If there is no buying order but the value meets the buy, then it is buying
            if (currentStatus == WatcherStatus.SET && buy >= value)
                return WatcherStatus.BUYING;

            // If there is no buying order but the value meets the buy, then it is buying
            if (currentStatus == WatcherStatus.BUYING && hasBuyingOrder)
                return WatcherStatus.BOUGHT;

            // If the buy is confirmed and the sell has no value,then it is holding
            if (currentStatus == WatcherStatus.BOUGHT && !sell.HasValue)
                return WatcherStatus.HOLDING;

            // If the sell has value, there is no selling order and the value meets the sell, then it is selling
            if (currentStatus == WatcherStatus.BOUGHT && sell.HasValue && sell <= value)
                return WatcherStatus.SELLING;

            // If the sell is confirmed
            if (currentStatus == WatcherStatus.SELLING && isSellingOrderConfirmed)
                return WatcherStatus.SOLD;

            // Default value
            return currentStatus;
        }
        public static bool BuildHasBuyingOrder(this Watcher watcher)
        {
            var hasBuyingOrder = watcher.BuyingOrder != null;
            return hasBuyingOrder;
        }
        public static bool BuildHasSellingOrder(this Watcher watcher)
        {
            var hasSellingOrder = watcher.SellingOrder != null;
            return hasSellingOrder;
        }
        public static bool BuildIsBuyingOrderConfirmed(this Watcher watcher)
        {
            var isBuyingOrderConfirmed = watcher.BuyingOrder != null && watcher.BuyingOrder.ExecutedAt.HasValue;
            return isBuyingOrderConfirmed;
        }
        public static bool BuildIsSellingOrderConfirmed(this Watcher watcher)
        {
            var isSellingOrderConfirmed = watcher.SellingOrder != null && watcher.SellingOrder.ExecutedAt.HasValue;
            return isSellingOrderConfirmed;
        }
    }
}
