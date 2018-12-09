using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class IndicatorExpression
    {
        public static Expression<Func<Indicator, bool>> Indicator(string userId, string name)
        {
            return x => 
                x.UserId == userId &&
                x.Name == name;
        }
        public static Expression<Func<Indicator, bool>> Filter(string userId)
        {
            return x => x.UserId == userId;
        }       
    }
}
