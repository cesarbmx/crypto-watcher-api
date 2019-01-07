using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class WatcherFakeResponse
    {
        public static WatcherResponse GetFake_PriceChange24HrsWatcher()
        {
            return new WatcherResponse
            {
                WatcherId = Guid.NewGuid(),
                UserId = "johny12",
                IndicatorType = IndicatorType.CurrencyIndicator,
                IndicatorId = "price-change-24hrs",
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
        public static WatcherResponse GetFake_HypeWatcher()
        {
            return new WatcherResponse
            {
                WatcherId = Guid.NewGuid(),
                UserId = "johny12",
                IndicatorType = IndicatorType.CurrencyIndicator,
                IndicatorId = "hype",
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
                GetFake_PriceChange24HrsWatcher(),
                GetFake_HypeWatcher()
            };
        }
    }
}
