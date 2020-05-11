using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Domain.Expressions
{
    public static class OrderExpression
    {
        public static Expression<Func<Order, bool>> Order(string userId, string currencyId, OrderType orderType)
        {
            return x => x.UserId == userId &&
                        x.CurrencyId == currencyId &&
                        x.OrderType == orderType;
        }
        public static Expression<Func<Order, bool>> OrderFilter(string userId = null)
        {
            return x => string.IsNullOrEmpty(userId) || x.UserId == userId;
        }
    }
}
