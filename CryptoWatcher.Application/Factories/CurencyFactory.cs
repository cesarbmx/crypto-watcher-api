using System;
using System.Collections.Generic;
using CoinpaprikaAPI.Entity;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Application.Factories
{
    public static class CurrencyFactory
    {
        public static List<Currency> Create(List<TickerWithQuotesInfo> tickers)
        {
            // Currencies
            var currencies = new List<Currency>();

            // Now
            var now = DateTime.Now;
            
            // For each ticker
            foreach (var ticker in tickers)
            {
                currencies.Add(new Currency(
                    ticker.Id,
                    ticker.Symbol,
                    ticker.Name, 
                    Convert.ToInt16(ticker.Rank),
                    ticker.Quotes["USD"].Price,
                    ticker.Quotes["USD"].Volume24H,
                    ticker.Quotes["USD"].MarketCap,
                    ticker.Quotes["USD"].PercentChange24H,
                    now));
            }

            // Return
            return currencies;
        }
       
    }
}
