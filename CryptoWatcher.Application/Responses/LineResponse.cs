using System;



namespace CryptoWatcher.Application.Responses
{
    public class LineResponse
    {
        public string CurrencyId { get; set; }
        public string IndicatorId { get; set; }
        public decimal Value { get; set; }
        public decimal? AverageBuy { get; set; }
        public decimal? AverageSell { get; set; }
        public DateTime Time { get; set; }
    }
}
