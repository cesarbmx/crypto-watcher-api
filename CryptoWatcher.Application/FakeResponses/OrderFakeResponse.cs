using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class OrderFakeResponse
    {
        public static OrderResponse GetFake_bitcoin()
        {
            return new OrderResponse
            {
                OrderId = Guid.NewGuid(),
                UserId = "johny12",
                CurrencyId = "bitcoin",
                OrderType = OrderType.BuyLimit,
                Status = OrderStatus.Pending,
                Quantity = 100
            };
        }
        public static OrderResponse GetFake_eos()
        {
            return new OrderResponse
            {
                OrderId = Guid.NewGuid(),
                UserId = "johny12",
                CurrencyId = "eos",
                OrderType = OrderType.BuyLimit,
                Status = OrderStatus.Pending,
                Quantity = 100
            };
        }
        public static List<OrderResponse> GetFake_List()
        {
            return new List<OrderResponse>
            {
                GetFake_bitcoin(),
                GetFake_eos()
            };
        }
    }
}
