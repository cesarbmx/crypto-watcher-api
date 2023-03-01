using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CesarBmx.Notification.Domain.Models;

namespace CesarBmx.Notification.Domain.Expressions
{
    public static class IndicatorExpression
    {
        public static Expression<Func<Indicator, bool>> Unique(string userId, string abbreviation)
        {
            return x =>
                x.UserId == userId &&
                x.Abbreviation == abbreviation;
        }
        public static Expression<Func<Indicator, bool>> Filter(string indicatorId = null, string userId = null)
        {
            return x =>  (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId) &&
                         (string.IsNullOrEmpty(userId) || x.UserId == userId);
        }
        public static Expression<Func<Indicator, bool>> Filter(List<string> indicatorIds)
        {
            return x =>  indicatorIds == null || !indicatorIds.Any() || indicatorIds.Contains(x.IndicatorId);
        }
    }
}
