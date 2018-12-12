using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class LineBuilder
    {
        public static List<Line> BuildLines(List<Currency> currencies, List<Indicator> indicators, List<Watcher> watchers)
        {
            var lines = new List<Line>();
            var time = DateTime.UtcNow;

            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    var value = IndicatorBuilder.BuildValue(currency, indicator, currencies, watchers);
                    var averageBuy = IndicatorBuilder.BuildAverageBuy(currency, indicator, watchers);
                    var averageSell = IndicatorBuilder.BuildAverageSell(currency, indicator, watchers);
                    var line = new Line(currency.Id, indicator.Id, value, averageBuy, averageSell, time);
                    lines.Add(line);
                }
            }

            // Return
            return lines;
        }       
    }
}
