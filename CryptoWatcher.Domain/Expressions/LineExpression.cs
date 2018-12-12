using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class LineExpression
    {
        public static Expression<Func<Line, bool>> Line(string currencyId, string indicatorId)
        {
            return x =>
                x.CurrencyId == currencyId &&
                x.IndicatorId == indicatorId;
        }      
    }
}
