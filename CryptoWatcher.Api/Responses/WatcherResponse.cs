using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Responses
{
    public class WatcherResponse
    {
        public string WatcherId { get; set; }
        public string UserId { get; set; }
        public Indicator IndicatorId { get; set; }
        public string CurrencyId { get; set; }
        public string OrderId { get; set; }
        public decimal IndicatorValue { get; set; }
        public WatcherSettings WatcherSettings { get; set; }
        public WatcherSettings WatcherSettingsTrend { get; set; }
        public bool WatcherEnabled { get; set; }
        public WatcherStatus WatcherStatus { get; set; }
    }
}
