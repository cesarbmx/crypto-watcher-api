

namespace CryptoWatcher.Domain.Models
{
    public class Currency
    {
        public string CurrencyId { get; private set; }
        public string Symbol { get; private set; }
        public string Name { get; private set; }
        public short Rank { get; private set; }
        public decimal Price { get; private set; }
        public decimal MarketCap { get; private set; }
        public decimal Volume24H { get; private set; }
        public decimal PercentageChange24H { get; private set; }

        public Currency() { }
        public Currency(
            string currencyId,
            string symbol,
            string name,
            short rank,
            decimal price,
            decimal volume24H,
            decimal marketCap,
            decimal percentageChange24H
        )
        {
            CurrencyId = currencyId;
            Symbol = symbol;
            Name = name;
            Rank = rank;
            Price = price;
            Volume24H = volume24H;
            MarketCap = marketCap;
            PercentageChange24H = percentageChange24H;
        }
    }
}
