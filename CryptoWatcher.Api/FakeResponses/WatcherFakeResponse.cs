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
                Id = "cesarbmx-bitcoin-price-change",
                UserId = "cesarbmx",
                CurrencyId = "bitcoin",
                IndicatorType = IndicatorType.PriceChange,
                IndicatorValue = 5000,
                Settings = new WatcherSettings(2, 15),
                SettingsTrend = 
                new WatcherSettings(2, 15),
                Status = WatcherStatus.Buy,
                Enabled = false
            };
        }
        public static WatcherResponse GetFake_HypeWatcher()
        {
            return new WatcherResponse
            {
                Id = "cesarbmx-bitcoin-hype",
                UserId = "cesarbmx",
                CurrencyId = "bitcoin",
                IndicatorType = IndicatorType.Hype,
                IndicatorValue = 2,
                Settings = new WatcherSettings(2, 15),
                SettingsTrend = new WatcherSettings(2, 15),
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
