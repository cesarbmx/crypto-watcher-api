using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class OrderExpression
    {
        public static Expression<Func<Order, bool>> Order(string userId, string currencyId, Guid watcherId, OrderType orderType)
        {
            return x => 
                x.UserId == userId &&
                x.CurrencyId == currencyId &&
                x.WatcherId == watcherId &&
                x.OrderType == orderType;
        }
        public static Expression<Func<Order, bool>> OrderFilter(string userId)
        {
            return x => string.IsNullOrEmpty(userId) || x.UserId == userId;
        }
    }
}
