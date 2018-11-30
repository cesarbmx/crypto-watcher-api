using System;
using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class OrderBuilder
    {
        public static List<Order> BuildNewOrders(this List<Watcher> watchers, List<Order> ongoingOrders)
        {
            var orders = new List<Order>();

            foreach (var watcher in watchers)
            {
                // Add if there are no similar orders
                var orderType = watcher.Status.BuildOrderType();
                var userOrders = ongoingOrders.Where(OrderExpression.UserOrder(
                    watcher.UserId,
                    watcher.CurrencyId,
                    orderType).Compile()).ToList();
                if (userOrders.Count == 0)
                {
                    var order = new Order(watcher.UserId, watcher.CurrencyId, orderType, 100);
                    orders.Add(order);
                }
            }

            // Return
            return orders;
        }

        public static OrderType BuildOrderType(this WatcherStatus watcherStatus)
        {
            switch (watcherStatus)
            {
                case WatcherStatus.Buy:
                    return OrderType.BuyLimit;
                case WatcherStatus.Sell:
                    return OrderType.SellMarket;
                default:
                    throw new ArgumentOutOfRangeException(nameof(watcherStatus), watcherStatus, null);
            }
        }
    }
}
