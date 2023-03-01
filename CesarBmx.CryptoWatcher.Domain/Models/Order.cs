using System;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Types;


namespace CesarBmx.CryptoWatcher.Domain.Models
{
    public class   Order
    {
        public int OrderId { get; private set; }
        public int WatcherId { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public decimal Price { get; private set; }
        public OrderType OrderType { get; private set; }
        public decimal Quantity { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }
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
            OrderId = 0;
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
