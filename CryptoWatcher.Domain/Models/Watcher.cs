using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Shared.Helpers;


namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {
        public string WatcherId => Id;
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public string IndicatorId { get; private set; }
        public decimal Value { get; private set; }
        public BuySell BuySell { get; private set; }
        public BuySell RecomendedBuySell { get; private set; }
        public bool Enabled { get; private set; }
        public WatcherStatus Status => WatcherBuilder.BuildStatus(Value, BuySell);

        public Watcher()
        {
            BuySell = new BuySell();
            RecomendedBuySell = new BuySell();
        }
        public Watcher(
            string userId,
            string currencyId,
            string indicatorId,
            decimal value,
            BuySell buySell,
            BuySell recommendedBuySell,
            bool enabled)
        : base(userId)
        {
            Id = UrlHelper.BuildUrl(indicatorId, currencyId); // Semantic id
            UserId = userId;
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            Value = value;
            BuySell = buySell;
            RecomendedBuySell = recommendedBuySell;
            Enabled = enabled;
        }

        public Watcher Update(BuySell buySell, bool enabled)
        {
            BuySell = buySell;
            Enabled = enabled;

            return this;
        }
        public Watcher Sync(decimal value, BuySell recomendedBuySell)
        {
            Value = value;
            RecomendedBuySell = recomendedBuySell;

            return this;
        }
    }
}
