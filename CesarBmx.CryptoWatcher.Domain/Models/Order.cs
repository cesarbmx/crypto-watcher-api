using System;

namespace CesarBmx.CryptoWatcher.Domain.Models
{
    public class Order
    {
        public Guid OrderId { get; private set; }
        public decimal? Price { get; private set; }
        public DateTime? ExecutedAt { get; private set; }

        public Order()
        {
            OrderId = Guid.NewGuid();
        }

        public Order ConfirmOrder(decimal price, DateTime executedAt)
        {
            Price = price;
            ExecutedAt = executedAt;

            return this;
        }
    }
}
