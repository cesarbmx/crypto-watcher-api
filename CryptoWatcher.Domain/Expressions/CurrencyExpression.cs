using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class CurrencyExpression
    {
        public static Expression<Func<Currency, bool>> Filter(string currencyId = null)
        {
            return x =>  string.IsNullOrEmpty(currencyId) || x.CurrencyId == currencyId;
        }       
    }
}
