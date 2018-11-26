

namespace CryptoWatcher.Domain.Models
{
    public class Order : Entity
    {
        public string OrderId { get; private set; }
        public OrderType OperationType { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public string WatcherId { get; private set; }
        public decimal OrderQuantity { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        public Order() { }
        public Order(
            OrderType operationType,
            string userId,
            string currencyId,
            string watcherId,
            decimal orderQuantity)
        {
            OrderId = Id;
            OperationType = operationType;
            UserId = userId;
            CurrencyId = currencyId;
            WatcherId = watcherId;
            OrderQuantity = orderQuantity;
            OrderStatus = OrderStatus.Pending;
        }
    }
}
