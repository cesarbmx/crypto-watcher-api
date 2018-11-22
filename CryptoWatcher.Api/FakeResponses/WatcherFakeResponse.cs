using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Api.FakeResponses
{
    public static class WatcherFakeResponse
    {
        public static WatcherResponse GetFake_PriceWatcher()
        {
            return new WatcherResponse
            {             
                UserId = "cesarbmx",
                CurrencyId = "bitcoin",
                WatcherType = WatcherType.Price,
                WatcherId = "cesarbmx-bitcoin-price",
                WatcherCurrentPrice = 5000,
                WatcherSettings = WatcherSettingsFakeResponse.GetFake_PriceWatcher(),
                WatcherSettingsTrend = WatcherSettingsFakeResponse.GetFake_PriceWatcher()
            };
        }
        public static WatcherResponse GetFake_HypeWatcher()
        {
            return new WatcherResponse
            {
                UserId = "cesarbmx",
                CurrencyId = "bitcoin",
                WatcherType = WatcherType.Hype,
                WatcherId = "cesarbmx-bitcoin-hype",
                WatcherCurrentPrice = 2,
                WatcherSettings = WatcherSettingsFakeResponse.GetFake_HypeWatcher(),
                WatcherSettingsTrend = WatcherSettingsFakeResponse.GetFake_HypeWatcher()
            };
        }
        public static List<WatcherResponse> GetFake_List()
        {
            return new List<WatcherResponse>
            {
                GetFake_PriceWatcher(),
                GetFake_HypeWatcher()
            };
        }
    }
}
