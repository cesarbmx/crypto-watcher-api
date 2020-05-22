using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class FakeOrder
    {
        public static Order GetFake_Bitcoin()
        {
            return new Order
            {
                OrderId = Guid.NewGuid(),
                OrderType = OrderType.BuyLimit,
                UserId = "master",
                CurrencyId = "bitcoin",
                OrderStatus = OrderStatus.Pending,
                Quantity = 100
            };
        }
        public static Order GetFake_EOS()
        {
            return new Order
            {
                OrderId = Guid.NewGuid(),
                OrderType = OrderType.BuyLimit,
                UserId = "master",
                CurrencyId = "eos",
                OrderStatus = OrderStatus.Pending,
                Quantity = 100
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
