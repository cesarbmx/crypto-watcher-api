using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class IndicatorDependencyExpression
    {
        public static Expression<Func<IndicatorDependency, bool>> IndicatorDependencyFilter(string indicatorId = null, string dependsOn = null)
        {
            return x => (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId) &&
                        (string.IsNullOrEmpty(dependsOn) || x.DependencyId == dependsOn);
        }       
    }
}
