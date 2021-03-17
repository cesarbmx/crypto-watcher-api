using System;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.Shared.Domain.Models;


namespace CesarBmx.CryptoWatcher.Domain.Models
{
    public class Currency: IEntity<Currency>
    {
        public string Id => CurrencyId;

        public string CurrencyId { get; private set; }
        public string Symbol { get; private set; }
        public string Name { get; private set; }
        public short Rank { get; private set; }
        public decimal Price { get; private set; }
        public decimal MarketCap { get; private set; }
        public decimal Volume24H { get; private set; }
        public decimal PercentageChange24H { get; private set; }
        public DateTime Time { get; private set; }

        public Currency() {}
        public Currency(
            string currencyId,
            string symbol,
            string name,
            short rank,
            decimal price,
            decimal volume24H,
            decimal marketCap,
            decimal percentageChange24H)
        {
            CurrencyId = currencyId;
            Symbol = symbol;
            Name = name;
            Rank = rank;
            Price = price;
            Volume24H = volume24H;
            MarketCap = marketCap;
            PercentageChange24H = percentageChange24H;
            Time = DateTime.UtcNow.StripSeconds();
        }

        public Currency Update(Currency currency)
        {
            Rank = currency.Rank;
            Price = currency.Price;
            MarketCap = currency.MarketCap;
            Volume24H = currency.Volume24H;
            PercentageChange24H = currency.PercentageChange24H;
            Time = currency.Time;

            return this;
        }
    }
}
