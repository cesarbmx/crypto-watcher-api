using CesarBmx.CryptoWatcher.Application.Requests;


namespace CesarBmx.CryptoWatcher.Application.FakeRequests
{
    public static class FakeSetWatcher
    {
        public static SetWatcher GetFake_1()
        {
            return new SetWatcher
            {
                WatcherId = 1,             
                Buy = 15,
                Sell = 8,
                Quantity = 100
            };
        }       
    }
}
