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
            var time = DateTime.Now;

            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    var value = IndicatorBuilder.BuildValue(currency, indicator, currencies);
                    var averageBuy = IndicatorBuilder.BuildAverageBuy(currency, indicator, watchers);
                    var averageSell = IndicatorBuilder.BuildAverageSell(currency, indicator, watchers);
                    var line = new Line(currency.CurrencyId, indicator.IndicatorId, value, averageBuy, averageSell, time);
                    lines.Add(line);
                }
            }

            // Return
            return lines;
        }
    }
}
