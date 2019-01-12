using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class WatcherFakeResponse
    {
        public static WatcherResponse GetFake_master_Bitcoin_Price()
        {
            return new WatcherResponse
            {
                WatcherId = "master_bitcoin_price",
                UserId = "master",
                IndicatorType = IndicatorType.CurrencyIndicator,
                IndicatorId = "price",
                TargetId = "bitcoin",
                Value = 5000,
                Buy = 15,
                Sell = 8,
                AverageBuy = 0,
                AverageSell = 0,
                Status = WatcherStatus.Buy,
                Enabled = false
            };
        }
        public static WatcherResponse GetFake_master_Bitcoin_RSI()
        {
            return new WatcherResponse
            {
                WatcherId = "master_bitcoin_rsi",
                UserId = "master",
                IndicatorType = IndicatorType.CurrencyIndicator,
                IndicatorId = "RSI",
                TargetId = "bitcoin",
                Value = 2,
                Buy = 15,
                Sell = 8,
                AverageBuy = 0,
                AverageSell = 0,
                Status = WatcherStatus.Sell,
                Enabled = false
            };
        }
        public static List<WatcherResponse> GetFake_List()
        {
            return new List<WatcherResponse>
            {
                GetFake_master_Bitcoin_Price(),
                GetFake_master_Bitcoin_RSI()
            };
        }
    }
}
