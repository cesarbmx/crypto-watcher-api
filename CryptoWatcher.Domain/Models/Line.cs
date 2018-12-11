using System;


namespace CryptoWatcher.Domain.Models
{
    public class Line
    {
        public string CurrencyId { get; private set; }
        public string IndicatorId { get; private set; }
        public decimal Value { get; private set; }
        public decimal RecommendedBuy { get; private set; }
        public decimal RecommendedSell { get; private set; }
        public DateTime Time { get; private set; }

        public Line() { }
        public Line(
            string currencyId,
            string indicatorId,
            decimal value,
            decimal recommendedBuy,
            decimal recommendedSell,
            DateTime time)
        {
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            Value = value;
            RecommendedBuy = recommendedBuy;
            RecommendedSell = recommendedSell;
            Time = time;
        } 
    }
}
