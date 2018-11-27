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
                IndicatorId = Indicator.PriceChange,
                CurrencyId = "bitcoin",
                IndicatorValue = 5000,
                WatcherSettings = new WatcherSettings(2, 15),
                WatcherSettingsTrend = new WatcherSettings(2, 15),
                WatcherEnabled = false,
                WatcherStatus = WatcherStatus.Buy
            };
        }
        public static WatcherResponse GetFake_HypeWatcher()
        {
            return new WatcherResponse
            {
                WatcherId = "cesarbmx-bitcoin-hype",
                UserId = "cesarbmx",
                IndicatorId = Indicator.Hype,
                CurrencyId = "bitcoin",
                IndicatorValue = 2,
                WatcherSettings = new WatcherSettings(2, 15),
                WatcherSettingsTrend = new WatcherSettings(2, 15),
                WatcherEnabled = false,
                WatcherStatus = WatcherStatus.Sell
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
