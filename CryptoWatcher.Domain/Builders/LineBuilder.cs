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
                    var value = IndicatorBuilder.BuildValue(currency, indicator.Id, currencies, watchers);
                    var line = new Line(currency.Id, indicator.Id, value, time);
                    lines.Add(line);
                }
            }

            // Return
            return lines;
        }       
    }
}
