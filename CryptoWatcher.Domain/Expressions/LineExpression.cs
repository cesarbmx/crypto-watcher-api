using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Builders;
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
        public static Expression<Func<Line, bool>> Filter(Period period, List<string> currencyIds, List<string> indicatorIds)
        {
            var userIds = IndicatorBuilder.BuildUserIds(indicatorIds);
            indicatorIds = IndicatorBuilder.BuildIndicatorIds(indicatorIds);

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
