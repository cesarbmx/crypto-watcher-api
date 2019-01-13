using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class IndicatorDependencyExpression
    {
        public static Expression<Func<IndicatorDependency, bool>> IndicatorDependency(string indicatorId, string dependsOn)
        {
            return x => x.IndicatorId == indicatorId && x.DependsOn == dependsOn;
        }
        public static Expression<Func<IndicatorDependency, bool>> IndicatorDependencyFilter(string indicatorId, string dependsOn)
        {
            return x => (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId) &&
                        (string.IsNullOrEmpty(dependsOn) || x.DependsOn == dependsOn);
        }       
    }
}
