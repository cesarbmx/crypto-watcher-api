using System;
using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Api.FakeResponses
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
                RecommendedBuySell = new BuySell(8,6),
                Time = DateTime.Now.AddHours(-1)
            };
        }
        public static LineResponse GetFake_2()
        {
            return new LineResponse
            {
                CurrencyId = "bitcoin",
                IndicatorId = "johny.melavo-bitcoin-hype",
                Value = 1.5m,
                RecommendedBuySell = new BuySell(9, 6),
                Time = DateTime.Now
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
