using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Domain.Types;

namespace CesarBmx.CryptoWatcher.Domain.Expressions
{
    public static class LineExpression
    {
        public static Expression<Func<Line, bool>> ObsoleteLine(LineRetention lineRetention)
        {
            return x => x.Period == Period.ONE_MINUTE && x.Time < DateTime.UtcNow.AddHours(-lineRetention[Period.ONE_MINUTE]) ||
                            x.Period == Period.FIVE_MINUTES && x.Time < DateTime.UtcNow.AddDays(-lineRetention[Period.FIVE_MINUTES]) ||
                            x.Period == Period.FIFTEEN_MINUTES && x.Time < DateTime.UtcNow.AddDays(-lineRetention[Period.FIFTEEN_MINUTES]) ||
                            x.Period == Period.ONE_HOUR && x.Time < DateTime.UtcNow.AddDays(-lineRetention[Period.ONE_HOUR]) ||
                            x.Period == Period.ONE_DAY && x.Time < DateTime.UtcNow.AddYears(-lineRetention[Period.ONE_DAY]);
        }
        public static Expression<Func<Line, bool>> Filter(Period? period, List<string> currencyIds, List<string> userIds, List<string> indicatorIds)
        {
            return x =>
                (
                    period == Period.ONE_DAY && x.Period == Period.ONE_DAY ||
                    period == Period.FIFTEEN_MINUTES && (x.Period == Period.ONE_DAY || x.Period == Period.FIFTEEN_MINUTES) ||
                    period == Period.FIVE_MINUTES && (x.Period == Period.ONE_DAY || x.Period == Period.FIFTEEN_MINUTES || x.Period == Period.FIVE_MINUTES) ||
                    period == Period.ONE_MINUTE
                ) &&
                (currencyIds == null || !currencyIds.Any() || currencyIds.Contains(x.CurrencyId)) &&
                (userIds == null || !userIds.Any() || userIds.Contains(x.UserId)) &&
                (indicatorIds == null || !indicatorIds.Any() || indicatorIds.Contains(x.IndicatorId));
        }
    }
}
