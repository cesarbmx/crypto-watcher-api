using CryptoWatcher.Application.Requests;



namespace CryptoWatcher.Application.FakeRequests
{
    public static class UpdateWatcherFakeRequest
    {
        public static UpdateWatcherRequest GetFake_1()
        {
            return new UpdateWatcherRequest
            {
                WatcherId = "master_bitcoin_rsi",             
                Buy = 15,
                Sell = 8,
                Enabled = true
            };
        }       
    }
}
