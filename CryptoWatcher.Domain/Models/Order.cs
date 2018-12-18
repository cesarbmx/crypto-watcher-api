using System;


namespace CryptoWatcher.Domain.Models
{
    public class Order : Entity
    {
        public string OrderId => Id;
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public string WatcherId { get; private set; }
        public OrderType OrderType { get; private set; }
        public decimal Quantity { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order() { }
        public Order(
            string userId,
            string currencyId,
            string watcherId,
            OrderType orderType,
            decimal quantity)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            CurrencyId = currencyId;
            WatcherId = watcherId;
            OrderType = orderType;
            UserId = userId;
            Quantity = quantity;
            Status = OrderStatus.Pending;
            CreatedBy = userId;
        }
    }
}
