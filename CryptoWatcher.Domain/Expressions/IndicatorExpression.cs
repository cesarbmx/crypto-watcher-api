using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Domain.Expressions
{
    public static class IndicatorExpression
    {
        public static Expression<Func<Indicator, bool>> Unique(string name)
        {
            return x => x.Name == name;
        }
        public static Expression<Func<Indicator, bool>> Filter(IndicatorType? indicatorType = null,  string indicatorId = null, string userId = null)
        {
            return x =>  (!indicatorType.HasValue || x.IndicatorType == indicatorType) &&
                         (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId) &&
                         (string.IsNullOrEmpty(userId) || x.UserId == userId);
        }       
    }
}
