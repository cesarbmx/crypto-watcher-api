using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class OrderFakeResponse
    {
        public static OrderResponse GetFake_Bitcoin()
        {
            return new OrderResponse
            {
                OrderId = Guid.NewGuid(),
                OrderType = OrderType.BuyLimit,
                UserId = "master",
                CurrencyId = "bitcoin",
                OrderStatus = OrderStatus.Pending,
                Quantity = 100
            };
        }
        public static OrderResponse GetFake_EOS()
        {
            return new OrderResponse
            {
                OrderId = Guid.NewGuid(),
                OrderType = OrderType.BuyLimit,
                UserId = "master",
                CurrencyId = "eos",
                OrderStatus = OrderStatus.Pending,
                Quantity = 100
            };
        }
        public static List<OrderResponse> GetFake_List()
        {
            return new List<OrderResponse>
            {
                GetFake_Bitcoin(),
                GetFake_EOS()
            };
        }
    }
}
