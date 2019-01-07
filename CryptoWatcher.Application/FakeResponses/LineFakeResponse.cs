using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class LineFakeResponse
    {
        public static DataPointResponse GetFake_CurrencyIndicator_1()
        {
            return new DataPointResponse
            {
                IndicatorType = IndicatorType.CurrencyIndicator,
                TargetId = "bitcoin",
                IndicatorId = "price-change-24hrs",
                UserId = "johny12",
                Value = 1.5m,
                AverageBuy = 15,
                AverageSell = 8,
                Time = DateTime.Now.AddHours(-1)
            };
        }
        public static DataPointResponse GetFake_CurrencyIndicator_2()
        {
            return new DataPointResponse
            {
                IndicatorType = IndicatorType.CurrencyIndicator,
                TargetId = "bitcoin",
                IndicatorId = "hype",
                UserId = "johny12",
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
                GetFake_CurrencyIndicator_1(),
                GetFake_CurrencyIndicator_2()
            };
        }
    }
}
