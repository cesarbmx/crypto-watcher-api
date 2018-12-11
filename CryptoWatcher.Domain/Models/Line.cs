using System;


namespace CryptoWatcher.Domain.Models
{
    public class Line
    {
        public string CurrencyId { get; private set; }
        public string IndicatorId { get; private set; }
        public decimal Value { get; private set; }
        public BuySell RecommendedBuySell { get; private set; }
        public DateTime Time { get; private set; }

        public Line() { }
        public Line(
            string currencyId,
            string indicatorId,
            decimal value, 
            BuySell recommendedBuySell,
            DateTime time)
        {
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            Value = value;
            RecommendedBuySell = recommendedBuySell;
            Time = time;
        } 
    }
}
