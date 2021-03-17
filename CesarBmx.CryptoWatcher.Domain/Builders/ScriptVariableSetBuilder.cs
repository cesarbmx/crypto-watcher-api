using System;
using System.Collections.Generic;
using System.Linq;
using CesarBmx.CryptoWatcher.Domain.Models;


namespace CesarBmx.CryptoWatcher.Domain.Builders
{
    public static class ScriptVariableSetBuilder
    {
        public static ScriptVariableSet BuildScriptVariableSet(List<Line> lines)
        {
            var scriptVariableSet = new ScriptVariableSet(
                BuildTimes(lines),
                BuildCurrencies(lines),
                BuildIndicators(lines),
                BuildValues(lines)
            );


            // Return
            return scriptVariableSet;
        }
        public static DateTime[] BuildTimes(List<Line> lines)
        {
            return lines.Select(x => x.Time).Distinct().ToArray();
        }
        public static string[] BuildCurrencies(List<Line> lines)
        {
            return lines.Select(x => x.CurrencyId).Distinct().ToArray();
        }
        public static string[] BuildIndicators(List<Line> lines)
        {
            return lines.Select(x => x.IndicatorId).Distinct().ToArray();
        }
        public static Dictionary<DateTime, Dictionary<string, Dictionary<string, decimal>>> BuildValues(List<Line> lines)
        {
            // Distinct
            var times = lines.Select(x => x.Time).Distinct().ToList();
            var indicatorIds = lines.Select(x => x.IndicatorId).Distinct().ToList();
            var currencyIds = lines.Select(x => x.CurrencyId).Distinct().ToList();

            // Loop
            var level1 = new Dictionary<DateTime, Dictionary<string, Dictionary<string, decimal>>>();
            foreach (var time in times)
            {
                var level2 = new Dictionary<string, Dictionary<string, decimal>>();
                foreach (var indicatorId in indicatorIds)
                {
                    var level3 = new Dictionary<string, decimal>();
                    foreach (var currencyId in currencyIds)
                    {
                        var line = lines.FirstOrDefault(x=>x.Time == time && x.CurrencyId == currencyId && x.IndicatorId == indicatorId);
                        if(line == null) continue;
                        level3.Add(currencyId, line.Value);
                    }
                    level2.Add(indicatorId, level3);
                }
                level1.Add(time, level2);
            }

            // Return
            return level1;
        }
    }
}
