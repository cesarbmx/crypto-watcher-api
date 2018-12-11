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
                WatcherId = "master-price-change-24hrs-bitcoin",             
                BuySell = new BuySell(15, 2),
                Enabled = true
            };
        }       
    }
}
