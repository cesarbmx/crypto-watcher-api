using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class FakeLine
    {
        public static Line GetFake_Bitcoin_Price()
        {
            return new Line
            {
                CurrencyId = "bitcoin",
                IndicatorId = "price",
                IndicatorType = IndicatorType.CurrencyIndicator,
                UserId = "master",
                Value = 1.5m,
                AverageBuy = 15,
                AverageSell = 8,
                Time = DateTime.Now.AddHours(-1)
            };
        }
        public static Line GetFake_Bitcoin_RSI()
        {
            return new Line
            {
                CurrencyId = "bitcoin",
                IndicatorId = "rsi",
                IndicatorType = IndicatorType.CurrencyIndicator,
                UserId = "master",
                Value = 1.5m,
                AverageBuy = 9,
                AverageSell = 6,
                Time = DateTime.Now
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
