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
                OrderId = null,
                IndicatorValue = 5000,
                WatcherSettings = WatcherSettingsFakeResponse.GetFake_PriceChangeWatcher(),
                WatcherSettingsTrend = WatcherSettingsFakeResponse.GetFake_PriceChangeWatcher(),
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
                OrderId = null,
                IndicatorValue = 2,
                WatcherSettings = WatcherSettingsFakeResponse.GetFake_HypeWatcher(),
                WatcherSettingsTrend = WatcherSettingsFakeResponse.GetFake_HypeWatcher(),
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
