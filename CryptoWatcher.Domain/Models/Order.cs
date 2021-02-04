using System;
using CesarBmx.Shared.Common.Extensions;
using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Domain.Models
{
    public class Order
    {
        public int OrderId { get; private set; }
        public OrderType OrderType { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public decimal Quantity { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }
        public DateTime? NotifiedAt { get; private set; }

        public Order() { }
        public Order(string userId, OrderType orderType, string currencyId, decimal quantity, DateTime createdAt)
        {
            OrderId = 0;
            OrderType = orderType;
            UserId = userId;
            CurrencyId = currencyId;
            UserId = userId;
            Quantity = quantity;
            OrderStatus = OrderStatus.PENDING;
            CreatedAt = createdAt;
            NotifiedAt = null;
        }

        public Order MarkAsFilled()
        {
            OrderStatus = OrderStatus.FILLED;
            ClosedAt = DateTime.UtcNow.StripSeconds();

            return this;
        }
        public Order MarkAsCancelled()
        {
            OrderStatus = OrderStatus.CANCELLED;
            ClosedAt = DateTime.UtcNow.StripSeconds();

            return this;
        }
        public Order MarkAsNotified()
        {
            NotifiedAt = DateTime.UtcNow.StripSeconds();

            return this;
        }
    }
}
