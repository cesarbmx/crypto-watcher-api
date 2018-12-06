

namespace CryptoWatcher.Domain.Models
{
    public class BuySell
    {
        public decimal BuyAt { get; set; }
        public decimal SellAt { get; set; }


        public BuySell(){}
        public BuySell(decimal buyAt, decimal sellAt)
        {
            BuyAt = buyAt;
            SellAt = sellAt;
        }
    }
}
