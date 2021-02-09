using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Domain.Builders
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
                if(watcher.UserId == "master") continue;

                // We skip hold watchers
                if (watcher.Status == WatcherStatus.HOLD) continue;

                // We add an order
                var orderType = BuildOrderType(watcher.Status);
                var order = new Order( 
                    watcher.WatcherId,
                    watcher.CreatorId,
                    watcher.CurrencyId,
                    orderType,
                    watcher.Amount??0m,
                    watcher.Price??0m,
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
