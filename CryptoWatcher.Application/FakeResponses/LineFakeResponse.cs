using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class LineFakeResponse
    {
        public static DataPointResponse GetFake_Bitcoin_Price()
        {
            return new DataPointResponse
            {
                TargetId = "bitcoin",
                IndicatorId = "price",
                IndicatorType = IndicatorType.CurrencyIndicator,
                UserId = "master",
                Value = 1.5m,
                AverageBuy = 15,
                AverageSell = 8,
                Time = DateTime.Now.AddHours(-1)
            };
        }
        public static DataPointResponse GetFake_Bitcoin_RSI()
        {
            return new DataPointResponse
            {
                TargetId = "bitcoin",
                IndicatorId = "rsi",
                IndicatorType = IndicatorType.CurrencyIndicator,
                UserId = "master",
                Value = 1.5m,
                AverageBuy = 9,
                AverageSell = 6,
                Time = DateTime.Now
            };
        }
        public static List<DataPointResponse> GetFake_List()
        {
            return new List<DataPointResponse>
            {
                GetFake_Bitcoin_Price(),
                GetFake_Bitcoin_RSI()
            };
        }
    }
}
