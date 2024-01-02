using System;
using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Domain.Types;
using CesarBmx.Shared.Application.Types;
using CesarBmx.Shared.Common.Extensions;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeWatcher
    {
        public static WatcherResponse GetFake_master_Bitcoin_Price()
        {
            return new WatcherResponse
            {
                WatcherId = 1,
                UserId = "cesarbmx",
                CurrencyId = "BTC",
                IndicatorUserId = "master",
                IndicatorId = "PRICE",
                Value = 5000,
                Buy = 15,
                Sell = 8,
                Quantity = 100,
                AverageBuy = 0,
                AverageSell = 0,
                Price = 5000,
                EntryAt = DateTime.UtcNow.StripSeconds(),
                EntryPrice = 14,
                ExitAt = DateTime.UtcNow.StripSeconds(),
                ExitPrice = 20,
                Status = WatcherStatus.BUYING,
                Enabled = false
            };
        }
        public static WatcherResponse GetFake_master_Bitcoin_RSI()
        {
            return new WatcherResponse
            {
                WatcherId = 2,
                UserId = "cesarbmx",
                CurrencyId = "BTC",
                IndicatorUserId = "master",
                IndicatorId = "RSI",
                Value = 2,
                Quantity = 200,
                Buy = 15,
                Sell = 8,
                AverageBuy = 0,
                AverageSell = 0,
                Price = 5000,
                EntryAt = DateTime.UtcNow.StripSeconds(),
                EntryPrice = 10,
                ExitAt = DateTime.UtcNow.StripSeconds(),
                ExitPrice = 30,
                Status = WatcherStatus.SELLING,
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
