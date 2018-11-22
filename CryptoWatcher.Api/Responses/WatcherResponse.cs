using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Responses
{
    public class WatcherResponse
    {
        public string WatcherId { get; set; }
        public string UserId { get; set; }
        public WatcherType WatcherType { get; set; }
        public string CurrencyId { get; set; }
        public decimal CurrentValue { get; set; }
        public WatcherSettings UserSettings { get; set; }
        public WatcherSettings TrendSettings { get; set; }
    }
}
