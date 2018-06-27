using System;
using System.Collections.Generic;
using NoobsMuc.Coinmarketcap.Client;

namespace Hyper.Infrastructure.Mappings
{
    public static class CurrencyMapping
    {       
        public static List<Domain.Models.Currency> Map(this IEnumerable<Currency> t)
        {
            // Initialize list
            var currencies = new List<Domain.Models.Currency>();

            // Map 
            foreach (var item in t)
            {
                var currency = item.Map();
                currencies.Add(currency);
            }

            // Return
            return currencies;
        }
        public static Domain.Models.Currency Map(this Currency t)
        {
            // Map
            var currency = new Domain.Models.Currency(
                t.Id,
                Convert.ToInt16(t.Rank),
                t.Name,
                Convert.ToDecimal(t.PriceUsd),
                Convert.ToDecimal(t.Volume24hUsd),
                Convert.ToDecimal(t.MarketCapUsd),
                Convert.ToDecimal(t.PercentChange24h));

            // Return
            return currency;
        }
    }
}
