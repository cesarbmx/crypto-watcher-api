using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CryptoWatcher.Application.Responses;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class FakeLine
    {
        public static Line GetFake_Bitcoin_Price()
        {
            return new Line
            {
                Time = DateTime.UtcNow.StripSeconds().StripSeconds().AddHours(-1),
                UserId = "master",
                CurrencyId = "btc",
                IndicatorId = "price",
                Value = 1.5m,
                AverageBuy = 15,
                AverageSell = 8
            };
        }
        public static Line GetFake_Bitcoin_RSI()
        {
            return new Line
            {
                Time = DateTime.UtcNow.StripSeconds().StripSeconds(),
                UserId = "master",
                CurrencyId = "btc",
                IndicatorId = "rsi",
                Value = 1.5m,
                AverageBuy = 9,
                AverageSell = 6
            };
        }
        public static List<Line> GetFake_List()
        {
            return new List<Line>
            {
                GetFake_Bitcoin_Price(),
                GetFake_Bitcoin_RSI()
            };
        }
    }
}
