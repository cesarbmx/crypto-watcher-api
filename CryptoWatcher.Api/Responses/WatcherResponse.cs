using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Responses
{
    public class WatcherResponse
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public IndicatorType IndicatorType { get; set; }
        public decimal IndicatorValue { get; set; }
        public WatcherSettings Settings { get; set; }
        public WatcherSettings SettingsTrend { get; set; }
        public WatcherStatus Status { get; set; }
        public bool Enabled { get; set; }
    }
}
