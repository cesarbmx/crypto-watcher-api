using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class LineExpression
    {
        public static Expression<Func<Line, bool>> Line(DateTime time, string targetId, string indicatorId)
        {
            return x => x.Time == time &&
                        x.TargetId == targetId &&
                        x.IndicatorId == indicatorId;
        }
        public static Expression<Func<Line, bool>> CurrentLine()
        {
            return x => x.IsCurrent;
        }
        public static Expression<Func<Line, bool>> ObsoleteLine()
        {
            return x => x.Time < DateTime.Now.AddDays(-7);
        }
        public static Expression<Func<Line, bool>> LineFilter(string currencyId = null, IndicatorType? indicatorType = null, string indicatorId = null, string userId = null)
        {
            return x => (string.IsNullOrEmpty(currencyId) || x.TargetId == currencyId) &&
                        (!indicatorType.HasValue || x.IndicatorType == indicatorType) &&
                        (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId) &&
                        (string.IsNullOrEmpty(userId) || x.UserId == userId);
        }
    }
}
