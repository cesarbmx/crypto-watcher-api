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
                Type = WatcherType.Price,
                CurrencyId = "bitcoin",
                CurrentValue = 5000,
                Settings = WatcherSettingsFakeResponse.GetFake_PriceWatcher(),
                TrendSettings = WatcherSettingsFakeResponse.GetFake_PriceWatcher()
            };
        }
        public static WatcherResponse GetFake_HypeWatcher()
        {
            return new WatcherResponse
            {
                UserId = "cesarbmx",
                Type = WatcherType.Hype,
                CurrencyId = "bitcoin",
                CurrentValue = 2,
                Settings = WatcherSettingsFakeResponse.GetFake_HypeWatcher(),
                TrendSettings = WatcherSettingsFakeResponse.GetFake_HypeWatcher()
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
