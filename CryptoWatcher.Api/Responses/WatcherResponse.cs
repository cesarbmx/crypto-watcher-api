using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Responses
{
    public class WatcherResponse
    {
        public string WatcherId { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public IndicatorType IndicatorType { get; set; }
        public decimal IndicatorValue { get; set; }
        public BuySell BuySell { get; set; }
        public BuySell RecommendedBuySell { get; set; }
        public WatcherStatus Status { get; set; }
        public bool Enabled { get; set; }
    }
}
