using System;
using System.Collections.Generic;
using System.Linq;
using CesarBmx.Shared.Common.Extensions;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Domain.Builders
{
    public static class LineBuilder
    {
        public static List<Line> BuildLines(List<Currency> currencies, List<Indicator> indicators, List<Watcher> watchers)
        {
            var time = DateTime.UtcNow.StripSeconds();
            var lines = new List<Line>();
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
                        var filteredWatchers = watchers.Where(WatcherExpression.Filter(null, currency.CurrencyId, indicator.IndicatorId).Compile()).ToList();
                        // Build
                        value = IndicatorBuilder.BuildValue(currency, indicator);
                        averageBuy = IndicatorBuilder.BuildAverageBuy(filteredWatchers);
                        averageSell = IndicatorBuilder.BuildAverageSell(filteredWatchers);
                    }

                    // Add
                    var line = new Line(time, indicator.UserId, currency.CurrencyId, indicator.IndicatorId, value, averageBuy, averageSell);
                    lines.Add(line);
                }
            }

            // Now we build the deeper levels
            if (stopAt > 0) BuildLines(currencies, indicators, watchers, lines, time, 1, stopAt);

            // Return
            return lines;
        }
        public static void BuildLines(List<Currency> currencies, List<Indicator> indicators, List<Watcher> watchers, List<Line> lines, DateTime time, int dependencyLevel, int stopAt)
        {
            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators.Where(x => x.DependencyLevel == dependencyLevel))
                {
                    // Get latest line for this currency indicator pair
                    var line = lines.FirstOrDefault(x => x.Time == time && x.CurrencyId == currency.CurrencyId && x.IndicatorId == indicator.IndicatorId);
                    // Get all watchers for this currency indicator pair
                    var filteredWatchers = watchers.Where(WatcherExpression.Filter(null, currency.CurrencyId, indicator.IndicatorId).Compile()).ToList();
                    // Build value and averages
                    var value = IndicatorBuilder.BuildValue(currency, indicator, lines);
                    var averageBuy = IndicatorBuilder.BuildAverageBuy(filteredWatchers);
                    var averageSell = IndicatorBuilder.BuildAverageSell(filteredWatchers);
                    // Set
                    line?.Set(value, averageBuy, averageSell);
                }
            }

            // Do the same with the next level recursively
            if (dependencyLevel < stopAt) BuildLines(currencies, indicators, watchers, lines, time, dependencyLevel + 1, stopAt);
        }
        public static Period BuildPeriod(DateTime time)
        {
            if (time.Hour == 0 &&
                time.Minute == 0) return Period.ONE_DAY;

            if (time.Minute == 0) return Period.ONE_HOUR;

            if (time.Minute % 15 == 0) return Period.FIFTEEN_MINUTES;

            if (time.Minute % 5 == 0) return Period.FIVE_MINUTES;

            return Period.ONE_MINUTE;
        }
    }
}
