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
        public decimal Buy { get; private set; }
        public decimal Sell { get; private set; }
        public decimal RecommendedBuy { get; private set; }
        public decimal RecommendedSell { get; private set; }
        public bool Enabled { get; private set; }
        public WatcherStatus Status => WatcherBuilder.BuildStatus(Value, Buy, Sell);

        public Watcher() { }
        public Watcher(
            string userId,
            string currencyId,
            string indicatorId,
            decimal value,
            decimal buy,
            decimal sell,
            decimal recommendedBuy,
            decimal recommendedSell,
            bool enabled)
        : base(userId)
        {
            Id = UrlHelper.BuildUrl(indicatorId, currencyId); // Semantic id
            UserId = userId;
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            Value = value;
            Buy = buy;
            Sell = sell;
            RecommendedBuy = recommendedBuy;
            RecommendedSell = recommendedSell;
            Enabled = enabled;
        }

        public Watcher Update(decimal buy, decimal sell, bool enabled)
        {
            Buy = buy;
            Sell = sell;
            Enabled = enabled;

            return this;
        }
        public Watcher Sync(decimal value, decimal recommendedBuy, decimal recommendedSell)
        {
            Value = value;
            RecommendedBuy = recommendedBuy;
            RecommendedSell = recommendedSell;

            return this;
        }
    }
}
