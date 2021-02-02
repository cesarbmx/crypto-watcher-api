using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Domain.Expressions
{
    public static class LineExpression
    {
        public static Expression<Func<Line, bool>> CurrentLine(DateTime time)
        {
            return x => x.Time == time;
        }
        public static Expression<Func<Line, bool>> ObsoleteLine()
        {
            return x => x.Period == Period.MINUTELY && x.Time < DateTime.UtcNow.AddHours(-1) ||
                            x.Period == Period.HOURLY && x.Time < DateTime.UtcNow.AddDays(-1) ||
                            x.Period == Period.MONTHLY && x.Time < DateTime.UtcNow.AddYears(-1) ||
                            x.Period == Period.YEARLY && x.Time < DateTime.UtcNow.AddYears(-1);
        }
        public static Expression<Func<Line, bool>> Filter(string currencyId = null, string indicatorId = null, string userId = null)
        {
            return x => (string.IsNullOrEmpty(currencyId) || x.CurrencyId == currencyId) &&
                        (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId) &&
                        (string.IsNullOrEmpty(userId) || x.UserId == userId);
        }
    }
}
