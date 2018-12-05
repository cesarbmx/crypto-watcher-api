

namespace CryptoWatcher.Domain.Models
{
    public class WatcherSettings
    {
        public decimal BuyAt { get; set; }
        public decimal SellAt { get; set; }


        public WatcherSettings(){}
        public WatcherSettings(decimal buyAt, decimal sellAt)
        {
            BuyAt = buyAt;
            SellAt = sellAt;
        }
    }
}
