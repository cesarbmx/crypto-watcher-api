using System;


namespace CryptoWatcher.Domain.Models
{
    public class HistoricalData
    {
        public string CurrencyId { get; private set; }
        public Indicator Indicator { get; private set; }
        public decimal Value { get; private set; }
        public BuySell RecommendedBuySell { get; private set; }
        public DateTime Time { get; private set; }

        public HistoricalData() { }
        public HistoricalData(
            string currencyId,
            Indicator indicator,
            decimal value, 
            BuySell recommendedBuySell,
            DateTime time)
        {
            CurrencyId = currencyId;
            Indicator = indicator;
            Value = value;
            RecommendedBuySell = recommendedBuySell;
            Time = time;
        } 
    }
}
