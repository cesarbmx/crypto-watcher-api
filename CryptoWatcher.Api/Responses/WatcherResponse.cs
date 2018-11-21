using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Responses
{
    public class WatcherResponse
    {
        public WatcherType WatcherType { get; set; }
        public WatcherSettings BuySellSettings { get; set; }
        public WatcherSettings TrendSettings { get; set; }
    }
}
