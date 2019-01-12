using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

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
                Status = OrderStatus.Pending,
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
                Status = OrderStatus.Pending,
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
