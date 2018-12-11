using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Responses
{
    public class WatcherResponse
    {
        public string WatcherId { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string IndicatorId { get; set; }
        public decimal Value { get; set; }
        public BuySell BuySell { get; set; }
        public BuySell RecommendedBuySell { get; set; }
        public WatcherStatus Status { get; set; }
        public bool Enabled { get; set; }
    }
}
