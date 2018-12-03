using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class OrderExpression
    {
        public static Expression<Func<Order, bool>> UserOrder(string userId)
        {
            return x => x.UserId == userId;
        }
        public static Expression<Func<Order, bool>> UserOrder(string userId, string currencyId)
        {
            return x => x.UserId == userId && x.CurrencyId == currencyId;
        }
        public static Expression<Func<Order, bool>> UserOrder(string userId, string currencyId, OrderType orderType)
        {
            return x => x.UserId == userId && x.CurrencyId == currencyId && x.OrderType == orderType;
        }
    }
}
