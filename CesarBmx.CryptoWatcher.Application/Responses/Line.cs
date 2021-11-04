using System;


namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class Line
    {
        public DateTime Time { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string IndicatorId { get; set; }
        public decimal? Value { get; set; }
        public decimal? AverageBuy { get; set; }
        public decimal? AverageSell { get; set; }
    }
}
