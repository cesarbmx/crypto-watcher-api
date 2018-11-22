using System.Collections.Generic;
using CryptoWatcher.Api.Responses;

namespace CryptoWatcher.Api.FakeResponses
{
    public static class CurrencyFakeResponse
    {
        public static CurrencyResponse GetFake_Bitcoin()
        {
            return new CurrencyResponse
            {              
                CurrencyId = "bitcoin",
                CurrencySymbol = "BTC",
                CurrencyName = "Bitcoin",
                CurrencyRank = 1,
                CurrencyPrice = (decimal)7464.36,
                CurrencyMarketCap = 128056880679,
                CurrencyVolume24H = 6715040000,
                CurrencyPercentageChange24H = (decimal)10.93
            };
        }
        public static CurrencyResponse GetFake_Ethereum()
        {
            return new CurrencyResponse
            {
                CurrencyId = "ethereum",
                CurrencySymbol = "ETH",
                CurrencyName = "Ethereum",
                CurrencyRank = 2,
                CurrencyPrice = (decimal)497.108,
                CurrencyMarketCap = 50092420102,
                CurrencyVolume24H = 2513170000,
                CurrencyPercentageChange24H = (decimal)5.61
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
