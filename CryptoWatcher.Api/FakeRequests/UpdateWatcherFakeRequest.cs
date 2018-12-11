using CryptoWatcher.Api.Requests;



namespace CryptoWatcher.Api.FakeRequests
{
    public static class UpdateWatcherFakeRequest
    {
        public static UpdateWatcherRequest GetFake_1()
        {
            return new UpdateWatcherRequest
            {
                WatcherId = "master-price-change-24hrs-bitcoin",             
                Buy = 15,
                Sell = 8,
                Enabled = true
            };
        }       
    }
}
