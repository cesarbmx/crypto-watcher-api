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
                OrderId = "2779cf8051-381f-4834-93dc-ece6345dde33",
                UserId = "johny.melavo",
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
                OrderId = "2779cf8051-381f-4834-93dc-ece6345dde44",
                UserId = "johny.melavo",
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
