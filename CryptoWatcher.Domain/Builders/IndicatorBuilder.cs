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
        public static decimal? BuildValue(Currency currency, Indicator indicator, List<Line> lines)
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
        public static List<Indicator> BuildDependencyLevels(List<Indicator> indicators, List<IndicatorDependency> dependencies)
        {
            foreach (var indicator in indicators)
            {
                var dependencyLevel = BuildDependencyLevel(indicator.IndicatorId, dependencies);
                indicator.SetDependencyLevel(dependencyLevel);
            }

            return indicators;
        }
        public static int BuildDependencyLevel(string indicatorId, List<IndicatorDependency> dependencies)
        {
            var dependencyLevel = -1;
            var indicatorDependencies = dependencies.Where(x => x.IndicatorId == indicatorId).ToList();

            foreach (var indicatorDependency in indicatorDependencies)
            {
                var result = BuildDependencyLevel(indicatorDependency.DependencyId, dependencies);
                if (result > dependencyLevel) dependencyLevel = result;
            }

            return dependencyLevel + 1;
        }
        public static int BuildDependencyLevel(List<Indicator> dependencies)
        {
            // Build
            var dependencyLevel = BuildMaxDependencyLevel(dependencies);

            // Return
            return dependencyLevel;
        }
        public static int BuildMaxDependencyLevel(List<Indicator> dependencies)
        {
            // Build
            var maxDependencyLevel = dependencies.Any() ? dependencies.Select(x => x.DependencyLevel).Max() + 1 : 0;

            // Return
            return maxDependencyLevel;
        }
        public static void BuildDependencies(List<Indicator> indicators, List<IndicatorDependency> indicatorDependencys)
        {
            foreach (var indicator in indicators)
            {
                var dependencies = indicatorDependencys.Where(x => x.IndicatorId == indicator.IndicatorId).ToList();
                indicator.SetDependencies(dependencies);
            }
        }
    }
}
