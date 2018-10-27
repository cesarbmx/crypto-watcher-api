﻿using System.Collections.Generic;
using Hyper.Api.Responses;

namespace Hyper.Api.FakeResponses
{
    public static class CurrencyFakeResponse
    {
        public static CurrencyResponse GetFake_Bitcoin()
        {
            return new CurrencyResponse
            {              
                Id = "bitcoin",
                Symbol = "BTC",
                Name = "Bitcoin",
                Rank = 1,
                Price = (decimal)7464.36,
                MarketCap = 128056880679,
                Volume24H = 6715040000,
                PercentageChange24H = (decimal)10.93
            };
        }
        public static CurrencyResponse GetFake_Ethereum()
        {
            return new CurrencyResponse
            {
                Id = "ethereum",
                Symbol = "ETH",
                Name = "Ethereum",
                Rank = 2,
                Price = (decimal)497.108,
                MarketCap = 50092420102,
                Volume24H = 2513170000,
                PercentageChange24H = (decimal)5.61
            };
        }
        public static List<CurrencyResponse> GetFake_List()
        {
            return new List<CurrencyResponse>
            {
                GetFake_Bitcoin(),
                GetFake_Ethereum()
            };
        }
    }
}
