using System;
using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class IndicatorBuilder
    {
        public static decimal BuildValue(Currency currency, Indicator indicator)
        {
            switch (indicator.IndicatorId)
            {
                case "price":
                    return currency.Price;
                case "price-change-24hrs":
                    return currency.PercentageChange24H;
                default:
                    return 666m;
            }            
        }
        public static decimal? BuildValue(Currency currency, Indicator indicator, List<DataPoint> lines)
        {
            var scriptVariables = ScriptVariablesBuilder.BuildScriptVariables(lines);
            switch (indicator.IndicatorId)
            {
                case "hype":
                    return BuildHypes(scriptVariables)[currency.CurrencyId];
                default:
                    return 666m;
            }
        }
        public static decimal BuildHypes(Currency currency, List<Currency> currencies)
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
        public static Dictionary<string, decimal?> BuildHypes(ScriptVariables scriptVariables)
        {
            // Arrange
            var time = scriptVariables.Times[0];
            var currencies = scriptVariables.Currencies;
            var values = scriptVariables.Values[time];

            var hypes = new Dictionary<string, decimal?>();

            foreach (var currency in currencies)
            {
                hypes.Add(currency, values[currency]["price-change-24hrs"]);
            }

            // Calculate
            var min = hypes.Values.Min();
            if (min < 0)   // If there are negatives, we move all the values to the right so we only deal with positives
            {
                foreach (var currency in currencies)
                {
                    hypes[currency] = hypes[currency] + min;
                }
            }
            
            var average = hypes.Values.Average(); // Average
            foreach (var currency in currencies)  // We pick the values above the average
            {
                hypes[currency] -= average;
                hypes[currency] = hypes[currency] < 0 ? 0 : hypes[currency]; // We set to zero the values below the average
            }

            // Return
            return hypes;
        }
        public static decimal? BuildAverageBuy(Currency currency, Indicator indicator, List<Watcher> watchers)
        {
            // Return null if there are no watchers
            if (watchers.Count == 0) return null;

            // Collect values
            var values = new decimal[watchers.Count];
            for (var i = 0; i < watchers.Count; i++)
            {
                var buy = watchers[i].Buy;
                if (!buy.HasValue) throw new ArgumentException("We expect watchers with buy sells");
                values[i] = buy.Value;
            }

            // Return
            return values.Average();
        }
        public static decimal? BuildAverageSell(Currency currency, Indicator indicator, List<Watcher> watchers)
        {
            // Return null if there are no watchers
            if (watchers.Count == 0) return null;

            // Collect values
            var values = new decimal[watchers.Count];
            for (var i = 0; i < watchers.Count; i++)
            {
                var sell = watchers[i].Sell;
                if (!sell.HasValue) throw new ArgumentException("We expect watchers with buy sells");
                values[i] = sell.Value;
            }

            // Return
            return values.Average();
        }
    }
}
