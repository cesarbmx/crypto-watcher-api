using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class IndicatorBuilder
    {
        public static decimal BuildValue(Currency currency, Indicator indicator, List<Currency> currencies)
        {
            switch (indicator.IndicatorId)
            {
                case "master-price-change-24hrs":
                    return currency.PercentageChange24H;
                case "master-hype":
                    return BuildHype(currency, currencies);
                default:
                    return 666m;
            }            
        }
        public static decimal BuildHype(Currency currency, List<Currency> currencies)
        {
            // Collect values
            var values = new decimal[currencies.Count];
            for (var i = 0; i < currencies.Count; i++)
            {
                values[i] = currencies[i].PercentageChange24H;
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
        public static decimal BuildAverageBuy(Currency currency, Indicator indicator, List<Watcher> watchers)
        {
            // Pick watchers for the given currency/indicator
            watchers = watchers.Where(WatcherExpression.WatcherFilter(currency.CurrencyId, indicator.IndicatorId).Compile()).ToList();

            // Return zero if there are no watchers
            if (watchers.Count == 0) return 0m;

            // Collect values
            var values = new decimal[watchers.Count];
            for (var i = 0; i < watchers.Count; i++)
            {
                values[i] = watchers[i].Buy;
            }

            // Return
            return values.Average();
        }
        public static decimal BuildAverageSell(Currency currency, Indicator indicator, List<Watcher> watchers)
        {
            // Pick watchers for the given currency/indicator
            watchers = watchers.Where(x => x.CurrencyId == currency.CurrencyId && x.IndicatorId == indicator.IndicatorId).ToList();

            // Return zero if there are no watchers
            if (watchers.Count == 0) return 0m;

            // Collect values
            var values = new decimal[watchers.Count];
            for (var i = 0; i < watchers.Count; i++)
            {
                values[i] = watchers[i].Sell;
            }

            // Return
            return values.Average();
        }
    }
}
