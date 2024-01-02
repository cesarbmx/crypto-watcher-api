using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeLine
    {
        public static LineResponse GetFake_Bitcoin_Price()
        {
            return new LineResponse
            {
                Time = DateTime.UtcNow.StripSeconds().AddHours(-1),
                UserId = "master",
                CurrencyId = "BTC",
                IndicatorId = "PRICE",
                Value = 1.5m,
                AverageBuy = 15,
                AverageSell = 8
            };
        }
        public static LineResponse GetFake_Bitcoin_RSI()
        {
            return new LineResponse
            {
                Time = DateTime.UtcNow.StripSeconds(),
                UserId = "master",
                CurrencyId = "BTC",
                IndicatorId = "RSI",
                Value = 1.5m,
                AverageBuy = 9,
                AverageSell = 6
            };
        }
        public static List<LineResponse> GetFake_List()
        {
            return new List<LineResponse>
            {
                GetFake_Bitcoin_Price(),
                GetFake_Bitcoin_RSI()
            };
        }
    }
}
