using CesarBmx.CryptoWatcher.Application.Requests;


namespace CesarBmx.CryptoWatcher.Application.FakeRequests
{
    public static class FakeEnableWatcher
    {
        public static EnableDisableWatcherRequest GetFake_1()
        {
            return new EnableDisableWatcherRequest
            {
                WatcherId = 1,
                Enabled = true
            };
        }       
    }
}
