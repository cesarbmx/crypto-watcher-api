using System;


namespace CryptoWatcher.Domain.Models
{
    public class Order : IEntity
    {
        public string Id => OrderId.ToString();
        public Guid OrderId { get; private set; }
        public OrderType OrderType { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public string WatcherId { get; private set; }
        public decimal Quantity { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime Time { get; private set; }

        public Order() { }
        public Order(
            string userId,
            OrderType orderType,
            string currencyId,
            string watcherId,
            decimal quantity)
        {
            OrderId = Guid.NewGuid();
            OrderType = orderType;
            UserId = userId;
            CurrencyId = currencyId;
            WatcherId = watcherId;
            UserId = userId;
            Quantity = quantity;
            Status = OrderStatus.Pending;
            Time = DateTime.Now;
        }
    }
}
