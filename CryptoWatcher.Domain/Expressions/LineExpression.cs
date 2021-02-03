using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Domain.Expressions
{
    public static class LineExpression
    {
        public static Expression<Func<Line, bool>> ObsoleteLine()
        {
            return x => x.Period == Period.ONE_MINUTE && x.Time < DateTime.UtcNow.AddHours(-3) ||
                            x.Period == Period.FIVE_MINUTES && x.Time < DateTime.UtcNow.AddDays(-1) ||
                            x.Period == Period.FIFTEEN_MINUTES && x.Time < DateTime.UtcNow.AddDays(-3) ||
                            x.Period == Period.ONE_HOUR && x.Time < DateTime.UtcNow.AddDays(-8) ||
                            x.Period == Period.ONE_DAY && x.Time < DateTime.UtcNow.AddYears(-1);
        }
        public static Expression<Func<Line, bool>> Filter(Period period, string currencyId = null, string indicatorId = null, string userId = null)
        {
            return x => 
                        (
                            period == Period.ONE_DAY && x.Period == Period.ONE_DAY ||
                            period == Period.FIFTEEN_MINUTES && (x.Period == Period.ONE_DAY || x.Period == Period.FIFTEEN_MINUTES) ||
                            period == Period.FIVE_MINUTES && (x.Period == Period.ONE_DAY || x.Period == Period.FIFTEEN_MINUTES || x.Period == Period.FIVE_MINUTES) ||
                            period == Period.ONE_MINUTE
                        ) &&
                        (string.IsNullOrEmpty(currencyId) || x.CurrencyId == currencyId) &&
                        (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId) &&
                        (string.IsNullOrEmpty(userId) || x.UserId == userId);
        }
    }
}
