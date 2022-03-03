using System.Collections.Generic;
using System.Linq;
using CesarBmx.CryptoWatcher.Domain.Models;


namespace CesarBmx.CryptoWatcher.Domain.Builders
{
    public static class IndicatorBuilder
    {
        public static decimal BuildValue(Currency currency, Indicator indicator, List<Line> lines)
        {
            switch (indicator.Abbreviation)
            {
                case "PRICE":
                    return currency.Price;
                case "PRICE_CHANGE_24H":
                    return currency.PercentageChange24H;
                case "HYPE":
                    var scriptVariables = ScriptVariablesBuilder.BuildScriptVariables(lines);
                    return BuildHypes(scriptVariables)[currency.CurrencyId];
                default:
                    return 666m;
            }            
        }
        public static Dictionary<string, decimal> BuildHypes(ScriptVariables scriptVariables)
        {
            // Arrange
            var hypes = new Dictionary<string, decimal>();
            var time = scriptVariables.Times[0];
            var currencies = scriptVariables.Values[time]["master.PRICE_CHANGE_24H"];
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
        public static List<Indicator> BuildDependencyLevels(List<Indicator> indicators, List<IndicatorDependency> dependencies)
        {
            // For each indicator
            foreach (var indicator in indicators)
            {
                var dependencyLevel = BuildDependencyLevel(indicator.IndicatorId, dependencies);
                indicator.SetDependencyLevel(dependencyLevel);
            }

            // Return
            return indicators;
        }
        public static int BuildDependencyLevel(string indicatorId, List<IndicatorDependency> dependencies)
        {
            var dependencyLevel = -1;
            var indicatorDependencies = dependencies.Where(x => x.IndicatorId == indicatorId).ToList();

            // For each dependency
            foreach (var indicatorDependency in indicatorDependencies)
            {
                var result = BuildDependencyLevel(indicatorDependency.DependencyId, dependencies);
                if (result > dependencyLevel) dependencyLevel = result;
            }

            // Return
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
        public static string BuildUserId(string indicatorId)
        {
            var split = indicatorId.Split(".");
            var userId = split[0];

            return userId;
        }
        public static string BuildIndicatorId(string indicatorId)
        {
            var split = indicatorId.Split(".");
            indicatorId = split[1];

            return indicatorId;
        }
    }
}
