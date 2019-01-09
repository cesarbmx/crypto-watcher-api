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
        public static Dictionary<string, decimal?> BuildHypes(ScriptVariables scriptVariables)
        {
            // Arrange
            var hypes = new Dictionary<string, decimal?>();
            var time = scriptVariables.Times[0];
            var currencies = scriptVariables.Values[time]["price-change-24hrs"];
            var values = currencies.Select(x=>x.Value).ToArray();

            // Build
            BuildHypes(values);
            var i = 0;
            foreach (var currency in currencies)
            {
                hypes.Add(currency.Key, values[i]);
                i++;
            }

            // Return
            return hypes;
        }
        public static void BuildHypes(decimal?[] values)
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
        public static decimal? BuildAverageBuy(List<Watcher> watchers)
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
        public static decimal? BuildAverageSell(List<Watcher> watchers)
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
        public static List<IndicatorDependency> BuildDependencies(string indicatorId, string[] dependencies)
        {
            var indicatorDependencies = new List<IndicatorDependency>();

            // Build
            foreach (var dependingIndicatorId in dependencies)
            {
                indicatorDependencies.Add(new IndicatorDependency(indicatorId, dependingIndicatorId));
            }

            // Return
            return indicatorDependencies;
        }
        public static int BuildDependencyLevel(List<IndicatorDependency> dependencies)
        {
            return dependencies.Select(x => x.Level).Max() ?? 0;
        }
    }
}
