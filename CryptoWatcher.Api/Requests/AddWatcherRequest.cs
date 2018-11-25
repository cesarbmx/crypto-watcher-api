
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.Requests
{
    public class AddWatcherRequest
    {
        public string UserId { get; set; }
        public WatcherType WatcherType { get; set; }
        public string CurrencyId { get; set; }
    }
}
