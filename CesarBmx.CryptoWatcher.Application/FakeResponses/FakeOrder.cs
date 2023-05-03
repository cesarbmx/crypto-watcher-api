using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Domain.Types;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeOrder
    {
        public static Order GetFake_Bitcoin()
        {
            return new Order
            {
                OrderId = Guid.NewGuid(),
                OrderType = OrderType.BUY,
                UserId = "master",
                CurrencyId = "BTC",
                OrderStatus = OrderStatus.PENDING,
                Price = 3200,
                Quantity = 100,
                SubmittedAt = DateTime.UtcNow.StripSeconds(),
                PlacedAt = null,
                FilledAt = null,
                CancelledAt = null,
                NotifiedAt = null
            };
        }
        public static Order GetFake_EOS()
        {
            return new Order
            {
                OrderId = Guid.NewGuid(),
                OrderType = OrderType.BUY,
                UserId = "master",
                CurrencyId = "EOS",
                OrderStatus = OrderStatus.PLACED,
                Price = 3.5m,
                Quantity = 100,
                SubmittedAt = DateTime.UtcNow.AddHours(-1).StripSeconds(),
                PlacedAt = DateTime.UtcNow.StripSeconds(),
                FilledAt = null,
                CancelledAt = null,
                NotifiedAt = null
            };
        }
        public static List<Order> GetFake_List()
        {
            return new List<Order>
            {
                GetFake_Bitcoin(),
                GetFake_EOS()
            };
        }
    }
}
