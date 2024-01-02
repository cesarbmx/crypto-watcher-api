using CesarBmx.CryptoWatcher.Application.Requests;


namespace CesarBmx.CryptoWatcher.Application.FakeRequests
{
    public static class FakeSetWatcher
    {
        public static SetWatcherRequest GetFake_1()
        {
            return new SetWatcherRequest
            {
                WatcherId = 1,             
                Buy = 30000,
                Sell = 50000,
                Quantity = 100
            };
        }       
    }
}
