

namespace CryptoWatcher.Domain.Models
{
    public class Order : Entity
    {
        public string OrderId { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public decimal Quantity { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        public Order() { }
        public Order(
            string userId,
            string currencyId,
            decimal quantity)
        {
            OrderId = Id;
            UserId = userId;
            CurrencyId = currencyId;
            Quantity = quantity;
            OrderStatus = OrderStatus.Pending;
        }
    }
}
