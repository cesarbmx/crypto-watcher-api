using CryptoWatcher.Api.Requests;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Api.FakeRequests
{
    public static class UpdateWatcherFakeRequest
    {
        public static UpdateWatcherRequest GetFake_1()
        {
            return new UpdateWatcherRequest
            {
                WatcherId = "cesarbmx-bitcoin-price-change",             
                BuySell = new BuySell(15, 2),
                Enabled = true
            };
        }       
    }
}
