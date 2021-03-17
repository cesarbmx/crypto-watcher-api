using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Domain.Expressions
{
    public static class IndicatorExpression
    {
        public static Expression<Func<Indicator, bool>> Filter(string indicatorId = null, string userId = null)
        {
            return x =>  (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId) &&
                         (string.IsNullOrEmpty(userId) || x.UserId == userId);
        }
        public static Expression<Func<Indicator, bool>> Filter(List<string> indicatorIds)
        {
            var userIds = IndicatorBuilder.BuildUserIds(indicatorIds);
            indicatorIds = IndicatorBuilder.BuildIndicatorIds(indicatorIds);

            return x => (userIds == null || !userIds.Any() || userIds.Contains(x.UserId)) &&
                        (indicatorIds == null || !indicatorIds.Any() || indicatorIds.Contains(x.IndicatorId));
        }
    }
}
