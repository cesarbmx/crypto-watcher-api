using System;
using CesarBmx.CryptoWatcher.Domain.Types;

namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public OrderType OrderType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public DateTime? NotifiedAt { get; set; }
    }
}
