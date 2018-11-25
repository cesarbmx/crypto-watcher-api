using System;
using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class IndicatorBuilder
    {
        public static decimal BuildIndicatorValue(Currency currency, Indicator indicator, List<Currency> currencies)
        {
            switch (indicator)
            {
                case Indicator.PriceChange:
                    return currency.CurrencyPercentageChange24H;
                case Indicator.Hype:
                    return BuildHype(currency, currencies);
                default:
                    throw new NotImplementedException();
            }            
        }
        public static decimal BuildHype(Currency currency, List<Currency> currencies)
        {
            // Collect values
            var values = new decimal[currencies.Count];
            for (var i = 0; i < currencies.Count; i++)
            {
                values[i] = currencies[i].CurrencyPercentageChange24H;
            }

            // Build hypes
            BuildHypes(values);

            // Return
            return values[currencies.IndexOf(currency)];
        }
        public static void BuildHypes(decimal[] values)
        {
            // If there are negatives, we move all the values to the right so we only deal with positives
            var min = values.Min();
            if (min < 0)
            {
                for (var i = 0; i < values.Length; i++)
                {
                    values[i] = values[i] + min;
                }
            }

            // We calculate the average
            var average = values.Average();

            // We pick the values above the average
            for (var i = 0; i < values.Length; i++)
            {
                values[i] -= average;
                // We set to zero the values below the average
                values[i] = values[i] < 0 ? 0 : values[i];
            }
        }
    }
}
