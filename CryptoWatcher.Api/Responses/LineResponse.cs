using System;



namespace CryptoWatcher.Api.Responses
{
    public class LineResponse
    {
        public string CurrencyId { get; set; }
        public string IndicatorId { get; set; }
        public decimal Value { get; set; }
        public decimal RecommendedBuy { get; set; }
        public decimal Recommendedell { get; set; }
        public DateTime Time { get; set; }
    }
}
