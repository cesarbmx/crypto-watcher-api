using CryptoWatcher.Api.Requests;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Api.FakeRequests
{
    public static class AddWatcherFakeRequest
    {
        public static AddWatcherRequest GetFake_1()
        {
            return new AddWatcherRequest
            {
                UserId = "johny.melavo",
                CurrencyId = "bitcoin",
                IndicatorId = "johny.melavo-hype",
                BuySell = new BuySell(8,2),
                Enabled = true
            };
        }       
    }
}
