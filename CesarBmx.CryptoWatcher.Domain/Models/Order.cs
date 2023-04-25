using System;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Types;


namespace CesarBmx.CryptoWatcher.Domain.Models
{
    public class   Order
    {
        public Guid OrderId { get; private set; }
        public int WatcherId { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public decimal Price { get; private set; }
        public OrderType OrderType { get; private set; }
        public decimal Quantity { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? PlacedAt { get; private set; }
        public DateTime? FilledAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }
        public DateTime? NotifiedAt { get; private set; }

        public Order() { }
        public Order(
            int watcherId,
            string userId,
            string currencyId,
            decimal price,
            decimal quantity,
            OrderType orderType,
            DateTime createdAt)
        {
            OrderId = Guid.NewGuid();
            WatcherId = watcherId;
            UserId = userId;
            CurrencyId = currencyId;
            Price = price;
            Quantity = quantity;
            OrderType = orderType;
            OrderStatus = OrderStatus.PENDING;
            CreatedAt = createdAt;
            NotifiedAt = null;
        }

        public Order MarkAsPlaced()
        {
            OrderStatus = OrderStatus.FILLED;
            PlacedAt = DateTime.UtcNow.StripSeconds();

            return this;
        }
        public Order MarkAsFilled()
        {
            OrderStatus = OrderStatus.FILLED;
            FilledAt = DateTime.UtcNow.StripSeconds();

            return this;
        }
        public Order MarkAsCancelled()
        {
            OrderStatus = OrderStatus.CANCELLED;
            CancelledAt = DateTime.UtcNow.StripSeconds();

            return this;
        }
        public Order MarkAsNotified()
        {
            NotifiedAt = DateTime.UtcNow.StripSeconds();

            return this;
        }
    }
}
