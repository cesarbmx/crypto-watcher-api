using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class LineExpression
    {
        public static Expression<Func<DataPoint, bool>> OldLine()
        {
            return x => x.Time < DateTime.Now.AddDays(-7);
        }
        public static Expression<Func<DataPoint, bool>> LineFilter(string currencyId = null, IndicatorType? indicatorType = null, string indicatorId = null, string userId = null)
        {
            return x => (string.IsNullOrEmpty(currencyId) || x.TargetId == currencyId) &&
                        (!indicatorType.HasValue || x.IndicatorType == indicatorType) &&
                        (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId) &&
                        (string.IsNullOrEmpty(userId) || x.UserId == userId);
        }
    }
}
