

namespace CryptoWatcher.Domain.Models
{
    public class Indicator
    {
        public string CurrencyId { get; private set; }
        public IndicatorType IndicatorType { get; private set; }
        public decimal Value { get; private set; }

        public Indicator() { }
        public Indicator(
            string currencyId,
            IndicatorType indicatorType,
            decimal value)
        {
            CurrencyId = currencyId;
            IndicatorType = indicatorType;
            Value = value;
        } 
    }
}
