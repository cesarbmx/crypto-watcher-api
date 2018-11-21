

namespace CryptoWatcher.Domain.Models
{
    public class WatcherSettings
    {
        public string CurrencyId { get; private set; }
        public decimal BuyAt { get; private set; }
        public decimal SellAt { get; private set; }


        public WatcherSettings() { }
        public WatcherSettings(string currencyId, decimal buyAt, decimal sellAt)
        {
            CurrencyId = currencyId;
            BuyAt = buyAt;
            SellAt = sellAt;
        }
    }
}
