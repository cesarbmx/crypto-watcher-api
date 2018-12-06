using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Shared.Helpers;


namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public IndicatorType IndicatorType { get; private set; }
        public decimal IndicatorValue { get; private set; }
        public BuySell BuySell { get; private set; }
        public BuySell RecomendedBuySell { get; private set; }
        public bool Enabled { get; private set; }
        public WatcherStatus Status => WatcherBuilder.BuildStatus(IndicatorValue, BuySell);

        public Watcher()
        {
            BuySell = new BuySell();
            RecomendedBuySell = new BuySell();
        }
        public Watcher(
            string userId,
            string currencyId,
            IndicatorType indicatorType,
            decimal indicatorValue,
            BuySell buySell,
            BuySell recommendedBuySell,
            bool enabled)
        {
            Id = UrlHelper.BuildUrl(userId, currencyId, indicatorType.ToString()); // Semantic id
            UserId = userId;
            CurrencyId = currencyId;
            IndicatorType = indicatorType;
            IndicatorValue = indicatorValue;
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
    }
}
