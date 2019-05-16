using System;
using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class LineBuilder
    {
        public static List<Line> BuildLines(List<Currency> currencies, List<Indicator> indicators, List<Watcher> watchers)
        {
            var lines = new List<Line>();
            var time = DateTime.Now;
            var stopAt = indicators.Count > 0 ? indicators.Max(x => x.DependencyLevel) : 0;

            // We create the lines in the order that is given. We build the first level (DependencyLevel = 0) for now
            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    decimal? value = null;
                    decimal? averageBuy = null;
                    decimal? averageSell = null;

                    if (indicator.DependencyLevel == 0) 
                    {
                        // Get all watchers for this currency indicator pair
                        var filteredWatchers = watchers.Where(WatcherExpression.WatcherFilter(null, currency.CurrencyId, indicator.IndicatorId).Compile()).ToList();
                        // Build
                        value = IndicatorBuilder.BuildValue(currency, indicator);
                        averageBuy = IndicatorBuilder.BuildAverageBuy(filteredWatchers);
                        averageSell = IndicatorBuilder.BuildAverageSell(filteredWatchers);
                    }

                    // Add
                    var line = new Line(currency.CurrencyId, indicator.IndicatorId, indicator.IndicatorType, indicator.UserId, value, averageBuy, averageSell, time);
                    lines.Add(line);
                }
            }

            // Now we build the deper levels
            if (stopAt > 0) BuildLines(currencies, indicators, watchers, lines, 1, stopAt);

            // Return
            return lines;
        }
        public static void BuildLines(List<Currency> currencies, List<Indicator> indicators, List<Watcher> watchers, List<Line> lines, int dependencyLevel, int stopAt)
        {
            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    if (indicator.DependencyLevel == dependencyLevel) // We set the consecutive ones
                    {
                        // Get latest line for this currency indicator pair
                        var line = lines.FirstOrDefault(LineExpression.Line(lines[0].Time, currency.CurrencyId, indicator.IndicatorId).Compile());
                        // Get all watchers for this currency indicator pair
                        var filteredWatchers = watchers.Where(WatcherExpression.WatcherFilter(null, currency.CurrencyId, indicator.IndicatorId).Compile()).ToList();
                        // Build
                        var value = IndicatorBuilder.BuildValue(currency, indicator, lines);
                        var averageBuy = IndicatorBuilder.BuildAverageBuy(filteredWatchers);
                        var averageSell = IndicatorBuilder.BuildAverageSell(filteredWatchers);
                        // Set
                        line?.Set(value, averageBuy, averageSell);
                    }
                }
            }

            // Do the same with the next level recursively
            if(dependencyLevel < stopAt)  BuildLines(currencies, indicators, watchers, lines, dependencyLevel + 1, stopAt);
        }
    }
}
