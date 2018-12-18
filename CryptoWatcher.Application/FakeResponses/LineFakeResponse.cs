using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class LineFakeResponse
    {
        public static LineResponse GetFake_1()
        {
            return new LineResponse
            {
                CurrencyId = "bitcoin",
                IndicatorId = "johny.melavo-price-change-24hrs-bitcoin",
                Value = 1.5m,
                AverageBuy = 15,
                AverageSell = 8,
                CreationTime = DateTime.Now.AddHours(-1)
            };
        }
        public static LineResponse GetFake_2()
        {
            return new LineResponse
            {
                CurrencyId = "bitcoin",
                IndicatorId = "johny.melavo-bitcoin-hype",
                Value = 1.5m,
                AverageBuy = 9,
                AverageSell = 6,
                CreationTime = DateTime.Now
            };
        }
        public static List<LineResponse> GetFake_List()
        {
            return new List<LineResponse>
            {
                GetFake_1(),
                GetFake_2()
            };
        }
    }
}
