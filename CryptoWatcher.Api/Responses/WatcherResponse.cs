using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Responses
{
    public class WatcherResponse
    {
        public string Id { get; set; }
        public WatcherType WatcherType { get; set; }
        public decimal CurrentValue { get; set; }
        public WatcherSettings Settings { get; set; }
        public WatcherSettings TrendSettings { get; set; }
    }
}
