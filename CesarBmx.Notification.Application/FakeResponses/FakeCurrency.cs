using System.Collections.Generic;
using CesarBmx.Notification.Application.Responses;

namespace CesarBmx.Notification.Application.FakeResponses
{
    public static class FakeCurrency
    {
        public static Currency GetFake_Bitcoin()
        {
            return new Currency
            {              
                CurrencyId = "BTC",
                Name = "Bitcoin",
                Rank = 1,
                Price = (decimal)7464.36,
                MarketCap = 128056880679,
                Volume24H = 6715040000,
                PercentageChange24H = (decimal)10.93
            };
        }
        public static Currency GetFake_Ethereum()
        {
            return new Currency
            {
                CurrencyId = "ETH",
                Name = "Ethereum",
                Rank = 2,
                Price = (decimal)497.108,
                MarketCap = 50092420102,
                Volume24H = 2513170000,
                PercentageChange24H = (decimal)5.61
            };
        }
        public static List<Currency> GetFake_List()
        {
            return new List<Currency>
            {
                GetFake_Bitcoin(),
                GetFake_Ethereum()
            };
        }
    }
}
