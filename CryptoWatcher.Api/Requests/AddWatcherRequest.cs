
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Requests
{
    public class AddWatcherRequest
    {
        public string UserId { get; set; }
        public Indicator Indicator { get; set; }
        public string CurrencyId { get; set; }
        public WatcherSettings WatcherSettings { get; set; }
    }
}
