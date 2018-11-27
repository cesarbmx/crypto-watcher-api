using CryptoWatcher.Api.Requests;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Api.FakeRequests
{
    public static class AddWatcherFakeRequest
    {
        public static AddWatcherRequest GetFake_1()
        {
            return new AddWatcherRequest
            {
                UserId = "cesarbmx",
                CurrencyId = "bitcoin",
                Indicator = Indicator.PriceChange,
                WatcherSettings = new WatcherSettings(15, 2)
            };
        }       
    }
}
