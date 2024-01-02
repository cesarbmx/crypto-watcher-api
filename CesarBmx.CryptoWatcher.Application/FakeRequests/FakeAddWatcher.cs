using CesarBmx.CryptoWatcher.Application.Requests;


namespace CesarBmx.CryptoWatcher.Application.FakeRequests
{
    public static class FakeAddWatcher
    {
        public static AddWatcherRequest GetFake_RSI()
        {
            return new AddWatcherRequest
            {
                UserId = "cesarbmx",
                CurrencyId = "BTC", 
                IndicatorId = "master.PRICE",
                Enabled = true
            };
        }       
    }
}
