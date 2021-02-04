using CryptoWatcher.Application.Requests;


namespace CryptoWatcher.Application.FakeRequests
{
    public static class UpdateWatcherFakeRequest
    {
        public static UpdateWatcher GetFake_1()
        {
            return new UpdateWatcher
            {
                WatcherId = 1,             
                Buy = 15,
                Sell = 8,
                Enabled = true
            };
        }       
    }
}
