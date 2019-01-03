using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class LineFakeResponse
    {
        public static LineResponse GetFake_CurrencyIndicator_1()
        {
            return new LineResponse
            {
                IndicatorType = IndicatorType.CurrencyIndicator,
                TargetId = "bitcoin",
                IndicatorId = "price-change-24hrs",
                UserId = "johny.melavo",
                Value = 1.5m,
                AverageBuy = 15,
                AverageSell = 8,
                Time = DateTime.Now.AddHours(-1)
            };
        }
        public static LineResponse GetFake_CurrencyIndicator_2()
        {
            return new LineResponse
            {
                IndicatorType = IndicatorType.CurrencyIndicator,
                TargetId = "bitcoin",
                IndicatorId = "hype",
                UserId = "johny.melavo",
                Value = 1.5m,
                AverageBuy = 9,
                AverageSell = 6,
                Time = DateTime.Now
            };
        }
        public static List<LineResponse> GetFake_List()
        {
            return new List<LineResponse>
            {
                GetFake_CurrencyIndicator_1(),
                GetFake_CurrencyIndicator_2()
            };
        }
    }
}
