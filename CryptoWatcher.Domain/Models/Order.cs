

namespace CryptoWatcher.Domain.Models
{
    public class Order : Entity
    {
        public string OrderId { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public OrderType OorderType { get; private set; }
        public decimal OrderQuantity { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        public Order() { }
        public Order(
            string userId,
            string currencyId,
            OrderType oorderType,
            decimal orderQuantity)
        {
            OrderId = Id;
            OorderType = oorderType;
            UserId = userId;
            CurrencyId = currencyId;
            OrderQuantity = orderQuantity;
            OrderStatus = OrderStatus.Pending;
        }
    }
}
