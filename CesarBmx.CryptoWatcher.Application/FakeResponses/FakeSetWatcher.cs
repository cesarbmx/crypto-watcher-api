using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Domain.Types;
using CesarBmx.Shared.Common.Extensions;
using System;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeSetWatcher
    {
        public static WatcherResponse GetFake_1()
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
    }
}
