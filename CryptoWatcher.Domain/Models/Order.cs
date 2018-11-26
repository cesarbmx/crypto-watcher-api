

namespace CryptoWatcher.Domain.Models
{
    public class Order : Entity
    {
        public string OrderId { get; private set; }
        public OperationType OperationType { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public string WatcherId { get; private set; }
        public decimal Quantity { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        public Order() { }
        public Order(
            OperationType operationType,
            string userId,
            string currencyId,
            string watcherId,
            decimal quantity)
        {
            OrderId = Id;
            OperationType = operationType;
            UserId = userId;
            CurrencyId = currencyId;
            WatcherId = watcherId;
            Quantity = quantity;
            OrderStatus = OrderStatus.Pending;
        }
    }
}
