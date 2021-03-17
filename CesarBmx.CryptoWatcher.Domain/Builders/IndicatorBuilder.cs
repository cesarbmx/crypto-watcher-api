using System.Collections.Generic;
using System.Linq;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;


namespace CesarBmx.CryptoWatcher.Domain.Builders
{
    public static class IndicatorBuilder
    {
        public static decimal BuildValue(Currency currency, Indicator indicator, List<Line> lines)
        {
            switch (indicator.IndicatorId)
            {
                case "PRICE":
                    return currency.Price;
                case "PRICE_CHANGE_24hrs":
                    return currency.PercentageChange24H;
                case "HYPE":
                    var scriptVariableSet = ScriptVariableSetBuilder.BuildScriptVariableSet(lines);
                    return BuildHypes(scriptVariableSet)[currency.CurrencyId];
                default:
                    return 666m;
            }            
        }
        public static Dictionary<string, decimal> BuildHypes(ScriptVariableSet scriptVariableSet)
        {
            // Arrange
            var hypes = new Dictionary<string, decimal>();
            var time = scriptVariableSet.Times[0];
            var currencies = scriptVariableSet.Values[time]["PRICE_CHANGE_24hrs"];
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
        public static decimal? BuildAverageBuy(List<Watcher> watchers)
        {
            // Watchers willing to buy
            var watcherWillingToBuy = watchers.Where(WatcherExpression.WatcherWillingToBuy()).ToList();
            
            // Buys
            var buys = watcherWillingToBuy.Where(x => x.Buy != null).Select(x => x.Buy);

            // Average
            var average = buys.Average();

            // Return
            return average;
        }
        public static decimal? BuildAverageSell(List<Watcher> watchers)
        {
            // Watchers willing to sell
            var watcherWillingToSell = watchers.Where(WatcherExpression.WatcherWillingToSell()).ToList();

            // Sells
            var sells = watcherWillingToSell.Where(x => x.Buy != null).Select(x => x.Buy);

            // Average
            var average = sells.Average();

            // Return
            return average;
        }
        public static List<Indicator> BuildDependencyLevels(List<Indicator> indicators, List<IndicatorDependency> dependencies)
        {
            // For each indicator
            foreach (var indicator in indicators)
            {
                var dependencyLevel = BuildDependencyLevel(indicator.UserId, indicator.IndicatorId, dependencies);
                indicator.SetDependencyLevel(dependencyLevel);
            }

            // Return
            return indicators;
        }
        public static int BuildDependencyLevel(string userId, string indicatorId, List<IndicatorDependency> dependencies)
        {
            var dependencyLevel = -1;
            var indicatorDependencies = dependencies.Where(x => x.UserId == userId && x.IndicatorId == indicatorId).ToList();

            // For each dependency
            foreach (var indicatorDependency in indicatorDependencies)
            {
                var result = BuildDependencyLevel(userId, indicatorDependency.DependencyIndicatorId, dependencies);
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
        public static void BuildDependencies(List<Indicator> indicators, List<IndicatorDependency> indicatorDependencies)
        {
            // For each indicator
            foreach (var indicator in indicators)
            {
                var dependencies = indicatorDependencies.Where(x => x.IndicatorId == indicator.IndicatorId).ToList();
                indicator.SetDependencies(dependencies);
            }
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
