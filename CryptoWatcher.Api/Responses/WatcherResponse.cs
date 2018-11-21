using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Responses
{
    public class WatcherResponse
    {
        public string UserId { get; set; }
        public WatcherType Type { get; set; }
        public string CurrencyId { get; set; }
        public decimal CurrentValue { get; set; }
        public WatcherSettings Settings { get; set; }
        public WatcherSettings TrendSettings { get; set; }
    }
}
