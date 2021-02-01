using CryptoWatcher.Application.Requests;


namespace CryptoWatcher.Application.FakeRequests
{
    public static class AddWatcherFakeRequest
    {
        public static AddWatcher GetFake_RSI()
        {
            return new AddWatcher
            {
                UserId = "master",
                IndicatorId = "rsi",
                CurrencyId = "bitcoin",
                Buy = 8m,
                Sell = 2m,
                Enabled = true
            };
        }       
    }
}
