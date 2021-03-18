using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Domain.Types;


namespace CesarBmx.CryptoWatcher.Domain.Builders
{
    public static class OrderBuilder
    {
        public static List<Order> BuildNewOrders(List<Watcher> watchers)
        {
            // Now
            var now = DateTime.UtcNow.StripSeconds();

            // New orders
            var newOrders = new List<Order>();

            // For each watcher
            foreach (var watcher in watchers)
            {
                // We skip default watchers
                if(watcher.UserId == "Master") continue;

                // We skip hold watchers
                if (watcher.Status == WatcherStatus.HOLD) continue;

                // We add an order
                var orderType = BuildOrderType(watcher.Status);
                var order = new Order( 
                    watcher.WatcherId,
                    watcher.UserId,
                    watcher.CurrencyId,
                    watcher.Price??0m,
                    watcher.Quantity ?? 0m,
                    orderType,
                    now);
                newOrders.Add(order);
            }

            // Return
            return newOrders;
        }
        public static OrderType BuildOrderType(WatcherStatus watcherStatus)
        {
            switch (watcherStatus)
            {
                case WatcherStatus.BUY:
                    return OrderType.BUY;
                case WatcherStatus.SELL:
                    return OrderType.SELL;
                default:
                    throw new ArgumentOutOfRangeException(nameof(watcherStatus), watcherStatus, null);
            }
        }
    }
}
