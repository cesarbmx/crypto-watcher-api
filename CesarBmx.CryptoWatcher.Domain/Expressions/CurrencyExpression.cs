﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Domain.Expressions
{
    public static class CurrencyExpression
    {
        public static Expression<Func<Currency, bool>> Filter(List<string> currencyIds)
        {
            return x => currencyIds == null || !currencyIds.Any() || currencyIds.Contains(x.CurrencyId);
        }       
    }
}
