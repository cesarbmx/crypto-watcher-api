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
                WatcherId = "cesarbmx-bitcoin-price-change",
                UserId = "cesarbmx",
                CurrencyId = "bitcoin",
                IndicatorId = Indicator.PriceChange,
                IndicatorValue = 5000,
                WatcherSettings = new WatcherSettings(2, 15),
                WatcherSettingsTrend = 
                new WatcherSettings(2, 15),
                WatcherStatus = WatcherStatus.Buy,
                WatcherEnabled = false
            };
        }
        public static WatcherResponse GetFake_HypeWatcher()
        {
            return new WatcherResponse
            {
                WatcherId = "cesarbmx-bitcoin-hype",
                UserId = "cesarbmx",
                CurrencyId = "bitcoin",
                IndicatorId = Indicator.Hype,
                IndicatorValue = 2,
                WatcherSettings = new WatcherSettings(2, 15),
                WatcherSettingsTrend = new WatcherSettings(2, 15),
                WatcherStatus = WatcherStatus.Sell,
                WatcherEnabled = false
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
