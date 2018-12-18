using System;
using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class LineBuilder
    {
        public static List<Line> BuildLines(List<Currency> currencies, List<Indicator> indicators, List<Watcher> watchers)
        {
            var lines = new List<Line>();
            var creationTime = DateTime.Now;

            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    var value = IndicatorBuilder.BuildValue(currency, indicator, currencies);
                    var averageBuy = IndicatorBuilder.BuildAverageBuy(currency, indicator, watchers);
                    var averageSell = IndicatorBuilder.BuildAverageSell(currency, indicator, watchers);
                    var line = new Line(currency.CurrencyId, indicator.IndicatorId, value, averageBuy, averageSell, creationTime);
                    lines.Add(line);
                }
            }

            // Return
            return lines;
        }
        public static List<Line> FilterLines(this List<Line> lines, string currencyId, string indicatorId)
        {
            if (!string.IsNullOrEmpty(currencyId))
                lines = lines.Where(x => x.CurrencyId == currencyId).ToList();
            if (!string.IsNullOrEmpty(indicatorId))
                lines = lines.Where(x => x.IndicatorId == indicatorId).ToList();

            return lines;
        }
    }
}
