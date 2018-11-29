

using System;

namespace CryptoWatcher.Domain.Models
{
    public class Order : Entity
    {
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public OrderType Type { get; private set; }
        public decimal Quantity { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order() { }
        public Order(
            string userId,
            string currencyId,
            OrderType type,
            decimal quantity)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            CurrencyId = currencyId;
            Type = type;
            UserId = userId;
            Quantity = quantity;
            Status = OrderStatus.Pending;
        }
    }
}
