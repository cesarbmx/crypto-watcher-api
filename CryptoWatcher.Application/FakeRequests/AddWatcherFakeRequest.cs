using CryptoWatcher.Application.Requests;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Application.FakeRequests
{
    public static class AddWatcherFakeRequest
    {
        public static AddWatcherRequest GetFake_RSI()
        {
            return new AddWatcherRequest
            {
                UserId = "master",
                IndicatorType = IndicatorType.CurrencyIndicator,
                IndicatorId = "rsi",
                TargetId = "bitcoin",
                Buy = 8m,
                Sell = 2m,
                Enabled = true
            };
        }       
    }
}
