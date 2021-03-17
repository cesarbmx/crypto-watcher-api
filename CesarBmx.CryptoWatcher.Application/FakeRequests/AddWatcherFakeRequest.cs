using CesarBmx.CryptoWatcher.Application.Requests;


namespace CesarBmx.CryptoWatcher.Application.FakeRequests
{
    public static class AddWatcherFakeRequest
    {
        public static AddWatcher GetFake_RSI()
        {
            return new AddWatcher
            {
                UserId = "cesarbmx",
                CurrencyId = "btc",
                CreatorId = "master",
                IndicatorId = "price",
                Enabled = true
            };
        }       
    }
}
