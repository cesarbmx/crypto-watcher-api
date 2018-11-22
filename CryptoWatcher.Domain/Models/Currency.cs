

namespace CryptoWatcher.Domain.Models
{
    public class Currency
    {
        public string CurrencyId { get; private set; }
        public string CurrencySymbol { get; private set; }
        public string CurrencyName { get; private set; }
        public short CurrencyRank { get; private set; }
        public decimal CurrencyPrice { get; private set; }
        public decimal CurrencyMarketCap { get; private set; }
        public decimal CurrencyVolume24H { get; private set; }
        public decimal CurrencyPercentageChange24H { get; private set; }

        public Currency() { }
        public Currency(
            string currencyId,
            string currencySymbol,
            string currencyName,
            short currencyRank,
            decimal currencyPrice,
            decimal currencyVolume24H,
            decimal currencyMarketCap,
            decimal currencyPercentageChange24H
        )
        {
            CurrencyId = currencyId;
            CurrencySymbol = currencySymbol;
            CurrencyName = currencyName;
            CurrencyRank = currencyRank;
            CurrencyPrice = currencyPrice;
            CurrencyVolume24H = currencyVolume24H;
            CurrencyMarketCap = currencyMarketCap;
            CurrencyPercentageChange24H = currencyPercentageChange24H;
        }
    }
}
