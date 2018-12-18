using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class LineExpression
    {
        public static Expression<Func<Line, bool>> Filter(string currencyId, string indicatorId)
        {
            return x =>
                string.IsNullOrEmpty(currencyId) && x.CurrencyId == currencyId ||
                x.IndicatorId == indicatorId;
        }
        public static Expression<Func<Line, bool>> ObsoleteLine()
        {
            return x => x.CreationTime < DateTime.Now.AddDays(-7);
        }
    }
}
