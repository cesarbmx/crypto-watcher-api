using CesarBmx.CryptoWatcher.Application.Requests;


namespace CesarBmx.CryptoWatcher.Application.FakeRequests
{
    public static class AddWatcherFakeRequest
    {
        public static AddWatcher GetFake_RSI()
        {
            return new AddWatcher
            {
                UserId = "CesarBmx",
                CurrencyId = "BTC",
                IndicatorUserId = "Master", 
                IndicatorId = "PRICE",
                Enabled = true
            };
        }       
    }
}
