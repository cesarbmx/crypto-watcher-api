using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Domain.Expressions
{
    public static class IndicatorExpression
    {
        public static Expression<Func<Indicator, bool>> Indicator(string indicatorId)
        {
            return x => x.IndicatorId == indicatorId;
        }
        public static Expression<Func<Indicator, bool>> IndicatorUnique(string name)
        {
            return x => x.Name == name;
        }
        public static Expression<Func<Indicator, bool>> IndicatorFilter(IndicatorType? indicatorType = null,  string indicatorId = null, string userId = null)
        {
            return x =>  (!indicatorType.HasValue || x.IndicatorType == indicatorType) &&
                         (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId) &&
                         (string.IsNullOrEmpty(userId) || x.UserId == userId);
        }       
    }
}
