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
                CurrencyId = "btc",
                OrderStatus = OrderStatus.PENDING,
                Quantity = 100,
                CreatedAt = DateTime.UtcNow.StripSeconds(),
                ClosedAt = null,
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
                CurrencyId = "eos",
                OrderStatus = OrderStatus.PENDING,
                Quantity = 100,
                CreatedAt = DateTime.UtcNow.StripSeconds(),
                ClosedAt = null,
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
