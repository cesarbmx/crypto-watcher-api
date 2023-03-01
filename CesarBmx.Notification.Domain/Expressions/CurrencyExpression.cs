using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CesarBmx.Notification.Domain.Models;

namespace CesarBmx.Notification.Domain.Expressions
{
    public static class CurrencyExpression
    {
        public static Expression<Func<Currency, bool>> Filter(List<string> currencyIds)
        {
            return x => currencyIds == null || !currencyIds.Any() || currencyIds.Contains(x.CurrencyId);
        }       
    }
}
