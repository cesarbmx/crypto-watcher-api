
using System;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Responses
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public OrderType OrderType { get; set; }
        public decimal Quantity { get; set; }
        public OrderStatus Status { get; set; }
    }
}
