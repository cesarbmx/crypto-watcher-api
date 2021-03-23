using CesarBmx.CryptoWatcher.Application.Requests;


namespace CesarBmx.CryptoWatcher.Application.FakeRequests
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
                Quantity = 100,
                Enabled = true
            };
        }       
    }
}
