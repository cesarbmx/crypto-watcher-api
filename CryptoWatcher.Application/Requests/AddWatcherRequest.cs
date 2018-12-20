


namespace CryptoWatcher.Application.Requests
{
    public class AddWatcherRequest 
    {
        public string UserId { get; set; }
        public string IndicatorId { get; set; }
        public string CurrencyId { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public bool Enabled { get; set; }
    }
}
