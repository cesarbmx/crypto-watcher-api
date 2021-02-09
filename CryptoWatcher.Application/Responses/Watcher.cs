using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Application.Responses
{
    public class Watcher
    {
        public int WatcherId { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string CreatorId { get; set; }
        public string IndicatorId { get; set; }
        public decimal Value { get; set; }
        public decimal? Buy { get; set; }
        public decimal? Sell { get; set; }
        public decimal? AverageBuy { get; set; }
        public decimal? AverageSell { get; set; }
        public decimal? Price { get; set; }
        public WatcherStatus Status { get; set; }
        public bool Enabled { get; set; }
    }
}
