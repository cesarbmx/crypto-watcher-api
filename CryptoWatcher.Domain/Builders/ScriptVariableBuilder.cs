using System;
using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class ScriptVariableBuilder
    {
        public static Dictionary<DateTime, Dictionary<IndicatorType, Dictionary<string, Dictionary<string, decimal>>>> BuildScriptVariables(List<DataPoint> lines)
        {
            // Distinct
            var time = lines.Select(x => x.Time).Distinct().ToList();
            var indicatorType = lines.Select(x => x.IndicatorType).Distinct().ToList();
            var targetId = lines.Select(x => x.TargetId).Distinct().ToList();
            var indicatorId = lines.Select(x => x.IndicatorId).Distinct().ToList();

            // Loop
            var level1 = new Dictionary<DateTime, Dictionary<IndicatorType, Dictionary<string, Dictionary<string, decimal>>>>();
            foreach (var timeKey in time)
            {
                var level2 = new Dictionary<IndicatorType, Dictionary<string, Dictionary<string, decimal>>>();
                foreach (var indicatorTypeKey in indicatorType)
                {
                    var level3 = new Dictionary<string, Dictionary<string, decimal>>();
                    foreach (var targetIdKey in targetId)
                    {
                        var level4 = new Dictionary<string, decimal>();
                        foreach (var indicatorIdKey in indicatorId)
                        {
                            var line = lines.FirstOrDefault(x => x.Time == timeKey &&
                                                        x.IndicatorType == indicatorTypeKey &&
                                                        x.TargetId == targetIdKey &&
                                                        x.IndicatorId == indicatorIdKey);
                            if (line != null) level4.Add(indicatorIdKey, line.Value);
                        }
                        level3.Add(targetIdKey, level4);
                    }
                    level2.Add(indicatorTypeKey, level3);
                }
                level1.Add(timeKey, level2);
            }

            // Return
            return level1;
        }
    }
}
