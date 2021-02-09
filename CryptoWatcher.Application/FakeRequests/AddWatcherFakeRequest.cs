using CryptoWatcher.Application.Requests;


namespace CryptoWatcher.Application.FakeRequests
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
