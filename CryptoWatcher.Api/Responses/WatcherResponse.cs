using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Responses
{
    public class WatcherResponse
    {
        public string WatcherId { get; set; }
        public string UserId { get; set; }
        public WatcherType WatcherType { get; set; }
        public string CurrencyId { get; set; }
        public decimal WatcherValue { get; set; }
        public WatcherSettings WatcherSettings { get; set; }
        public WatcherSettings WatcherSettingsTrend { get; set; }
        public bool WatcherEnabled { get; set; }
        public bool WatcherStatus { get; set; }
    }
}
