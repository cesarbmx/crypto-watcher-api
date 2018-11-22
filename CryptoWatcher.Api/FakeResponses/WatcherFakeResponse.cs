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
                WatcherValue = 5000,
                WatcherSettings = WatcherSettingsFakeResponse.GetFake_PriceWatcher(),
                WatcherSettingsTrend = WatcherSettingsFakeResponse.GetFake_PriceWatcher(),
                WatcherEnabled = false,
                WatcherStatus = true
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
                WatcherValue = 2,
                WatcherSettings = WatcherSettingsFakeResponse.GetFake_HypeWatcher(),
                WatcherSettingsTrend = WatcherSettingsFakeResponse.GetFake_HypeWatcher(),
                WatcherEnabled = false,
                WatcherStatus = true
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
