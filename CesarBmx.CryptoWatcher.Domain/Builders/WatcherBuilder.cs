using System;
using System.Collections.Generic;
using System.Linq;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Domain.Types;
using CesarBmx.Shared.Messaging.Ordering.Commands;
using CesarBmx.Shared.Messaging.Ordering.Types;

namespace CesarBmx.CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilder
    {
        public static WatcherStatus BuildStatus(Watcher watcher)
        {
            // Evaluate and return
            if (WatcherExpression.WatcherNotSet().Invoke(watcher)) return WatcherStatus.NOT_SET;
            if (WatcherExpression.WatcherBuying().Invoke(watcher)) return WatcherStatus.BUYING;
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
        public static List<PlaceOrder> BuildPlaceOrders(this List<Watcher> watchersWlillingToBuyOrSell)
        {
            var placeBuyOrders = new List<PlaceOrder>();

            // For each watcher willing to buy or sell
            foreach (var watcher in watchersWlillingToBuyOrSell)
            {
                // Build order type
                var orderType = watcher.BuildOrderType();

                // Build order id
                var orderId = watcher.BuildOrderId();

                // Command
                var placeOrder = new PlaceOrder
                {
                    OrderId = orderId,
                    OrderType = orderType,
                    UserId = watcher.UserId,
                    CurrencyId = watcher.CurrencyId,
                    Price = watcher.Price.Value,
                    Quantity = watcher.Quantity.Value
                };

                // Add
                placeBuyOrders.Add(placeOrder);
            }

            // Return
            return placeBuyOrders;
        }
        public static OrderType BuildOrderType(this Watcher watcher)
        {
            if (watcher.SellingOrder != null) return OrderType.SELL;
            if (watcher.BuyingOrder != null) return OrderType.BUY;
            throw new NotImplementedException();

        }
        public static Guid BuildOrderId(this Watcher watcher)
        {
            if (watcher.SellingOrder != null) return watcher.SellingOrder.OrderId;
            if (watcher.BuyingOrder != null) return watcher.BuyingOrder.OrderId;
            throw new NotImplementedException();

        }
        public static List<Watcher> SetAsBuying(this List<Watcher> watchers)
        {
            foreach (var watcher in watchers)
            {
                watcher.SetAsBuying();
            }
            return watchers;
        }
        public static List<Watcher> SetAsSelling(this List<Watcher> watchers)
        {
            foreach (var watcher in watchers)
            {
                watcher.SetAsSelling();
            }
            return watchers;
        }
    }
}
