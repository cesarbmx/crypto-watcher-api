using CryptoWatcher.Application.Requests;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Application.FakeRequests
{
    public static class AddWatcherFakeRequest
    {
        public static AddWatcherRequest GetFake_1()
        {
            return new AddWatcherRequest
            {
                UserId = "johny12",
                IndicatorType = IndicatorType.CurrencyIndicator,
                IndicatorId = "hype",
                TargetId = "bitcoin",
                Buy = 8m,
                Sell = 2m,
                Enabled = true
            };
        }       
    }
}
