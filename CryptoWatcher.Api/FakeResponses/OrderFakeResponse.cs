using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Api.FakeResponses
{
    public static class OrderFakeResponse
    {
        public static OrderResponse GetFake_bitcoin()
        {
            return new OrderResponse
            {
                OrderId = "2779cf8051-381f-4834-93dc-ece6345dde33",
                UserId = "cesarbmx",
                CurrencyId = "bitcoin",
                OrderType = OrderType.BuyLimit,
                OrderStatus = OrderStatus.Pending,
                OrderQuantity = 100
            };
        }
        public static OrderResponse GetFake_eos()
        {
            return new OrderResponse
            {
                OrderId = "2779cf8051-381f-4834-93dc-ece6345dde44",
                UserId = "cesarbmx",
                CurrencyId = "eos",
                OrderType = OrderType.BuyLimit,
                OrderStatus = OrderStatus.Pending,
                OrderQuantity = 100
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
