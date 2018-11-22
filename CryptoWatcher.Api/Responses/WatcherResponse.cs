using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Responses
{
    public class WatcherResponse
    {
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public WatcherType WatcherType { get; set; }
        public string WatcherId { get; set; }
        public decimal WatcherCurrentPrice { get; set; }
        public WatcherSettings WatcherSettings { get; set; }
        public WatcherSettings WatcherSettingsTrend { get; set; }
    }
}
