using System;


namespace CryptoWatcher.Domain.Models
{
    public class Order : IEntity
    {
        public string Id => OrderId.ToString();
        public Guid OrderId { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public Guid WatcherId { get; private set; }
        public OrderType OrderType { get; private set; }
        public decimal Quantity { get; private set; }
        public OrderStatus Status { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime Time { get; private set; }

        public Order() { }
        public Order(
            string userId,
            string currencyId,
            Guid watcherId,
            OrderType orderType,
            decimal quantity)
        {
            OrderId = Guid.NewGuid();
            UserId = userId;
            CurrencyId = currencyId;
            WatcherId = watcherId;
            OrderType = orderType;
            UserId = userId;
            Quantity = quantity;
            Status = OrderStatus.Pending;
            CreatedBy = userId;
            Time = DateTime.Now;
        }
    }
}
