using System;
using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class LineBuilder
    {
        public static List<DataPoint> BuildLines(List<Currency> currencies, List<Indicator> indicators, List<Watcher> watchers)
        {
            var lines = new List<DataPoint>();
            var time = DateTime.Now;
            var stopAt = indicators.Count > 0 ? indicators.Max(x => x.DependencyLevel) : 0;

            // We create the lines in the order that is given and we build the first level only
            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    decimal? value = null;
                    decimal? averageBuy = null;
                    decimal? averageSell = null;

                    if (indicator.DependencyLevel == 0) 
                    {
                        var filteredWatchers = watchers.Where(WatcherExpression.WatcherFilter(null, currency.CurrencyId, indicator.IndicatorId).Compile()).ToList();
                        value = IndicatorBuilder.BuildValue(currency, indicator);
                        averageBuy = IndicatorBuilder.BuildAverageBuy(filteredWatchers);
                        averageSell = IndicatorBuilder.BuildAverageSell(filteredWatchers);
                    }

                    var dataPoint = new DataPoint(indicator.IndicatorType, currency.CurrencyId, indicator.IndicatorId, indicator.UserId, value, averageBuy, averageSell, time);
                    lines.Add(dataPoint);
                }
            }

            // Now we build the deper levels
            if (stopAt > 0) BuildLines(currencies, indicators, watchers, lines, 1, stopAt);

            // Return
            return lines;
        }
        public static void BuildLines(List<Currency> currencies, List<Indicator> indicators, List<Watcher> watchers, List<DataPoint> lines, int dependencyLevel, int stopAt)
        {
            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    if (indicator.DependencyLevel == dependencyLevel) // We set the consecutive ones
                    {
                        var line = lines.FirstOrDefault(LineExpression.Line(lines[0].Time, currency.CurrencyId, indicator.IndicatorId).Compile());
                        var filteredWatchers = watchers.Where(WatcherExpression.WatcherFilter(null, currency.CurrencyId, indicator.IndicatorId).Compile()).ToList();
                        var value = IndicatorBuilder.BuildValue(currency, indicator, lines);
                        var averageBuy = IndicatorBuilder.BuildAverageBuy(filteredWatchers);
                        var averageSell = IndicatorBuilder.BuildAverageSell(filteredWatchers);
                        line?.Set(value, averageBuy, averageSell);
                    }
                }
            }
            if(dependencyLevel < stopAt)  BuildLines(currencies, indicators, watchers, lines, dependencyLevel + 1, stopAt);
        }
    }
}
