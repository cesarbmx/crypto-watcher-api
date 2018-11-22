

namespace CryptoWatcher.Domain.Models
{
    public class WatcherSettings
    {
        public decimal BuyAt { get; private set; }
        public decimal SellAt { get; private set; }


        public WatcherSettings(){}
        public WatcherSettings(decimal buyAt, decimal sellAt)
        {
            BuyAt = buyAt;
            SellAt = sellAt;
        }
    }
}
