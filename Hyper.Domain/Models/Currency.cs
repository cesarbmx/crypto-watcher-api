

namespace Hyper.Domain.Models
{
    public class Currency
    {
        public string Id { get; private set; }
        public short Rank { get; private set; }
        public string Name { get; private set; }
        public decimal MarketCap { get; private set; }
        public decimal Price { get; private set; }
        public decimal Volume24H { get; private set; }


        public decimal PercentageChange24H { get; private set; }

        public Currency() { }
        public Currency(
            string id,
            short rank,
            string name,
            decimal price,
            decimal volume24H,
            decimal marketCap,
            decimal percentageChange24H
        )
        {
            Id = id;
            Rank = rank;
            Name = name;
            Price = price;
            Volume24H = volume24H;
            MarketCap = marketCap;
            PercentageChange24H = percentageChange24H;
        }
    }
}
