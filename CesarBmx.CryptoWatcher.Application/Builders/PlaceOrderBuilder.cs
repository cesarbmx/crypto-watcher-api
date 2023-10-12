using System;
using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Messaging.Ordering.Commands;
using CesarBmx.Shared.Messaging.Ordering.Types;

namespace CesarBmx.CryptoWatcher.Application.Builders
{
    public static class PlaceOrderBuilder
    {
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
    }
}
