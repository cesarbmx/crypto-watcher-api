using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Api.FakeResponses
{
    public static class WatcherFakeResponse
    {
        public static WatcherResponse GetFake_PriceChangeWatcher()
        {
            return new WatcherResponse
            {
                WatcherId = "johny.melavo-bitcoin-price-change",
                UserId = "johny.melavo",
                CurrencyId = "bitcoin",
                IndicatorId = "johny.melavo-price-change",
                IndicatorValue = 5000,
                BuySell = new BuySell(2, 15),
                RecommendedBuySell = 
                new BuySell(2, 15),
                Status = WatcherStatus.Buy,
                Enabled = false
            };
        }
        public static WatcherResponse GetFake_HypeWatcher()
        {
            return new WatcherResponse
            {
                WatcherId = "johny.melavo-bitcoin-hype",
                UserId = "johny.melavo",
                CurrencyId = "bitcoin",
                IndicatorId = "johny.melavo-hype",
                IndicatorValue = 2,
                BuySell = new BuySell(2, 15),
                RecommendedBuySell = new BuySell(2, 15),
                Status = WatcherStatus.Sell,
                Enabled = false
            };
        }
        public static List<WatcherResponse> GetFake_List()
        {
            return new List<WatcherResponse>
            {
                GetFake_PriceChangeWatcher(),
                GetFake_HypeWatcher()
            };
        }
    }
}
