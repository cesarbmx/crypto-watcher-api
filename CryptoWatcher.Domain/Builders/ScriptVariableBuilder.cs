using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class ScriptVariableBuilder
    {
        public static Dictionary<string, Dictionary<string, Dictionary<string, decimal>>> BuildScriptVariables(List<DataPoint> lines)
        {
            var historicalData = new Dictionary<string, Dictionary<string, Dictionary<string, decimal>>>();

            foreach (var indicatorType in lines.Select(x=>x.IndicatorType).Distinct())
            {
                var level1 = new Dictionary<string, Dictionary<string, decimal>>();
                foreach (var targetId in lines.Select(x => x.TargetId).Distinct())
                {
                    var level2 = new Dictionary<string, decimal>();
                    foreach (var indicatorId in lines.Select(x => x.IndicatorId).Distinct())
                    {
                        foreach (var line in lines.Where(x=>x.IndicatorType == indicatorType &&
                                                             x.TargetId == targetId &&
                                                             x.IndicatorId == indicatorId))
                        {
                            level2.Add(indicatorId, line.Value);
                        }
                    }
                    level1.Add(targetId, level2);
                }
                historicalData.Add(indicatorType.ToString(), level1);
            }

            // Return
            return historicalData;
        }
    }
}
