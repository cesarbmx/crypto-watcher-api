using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Domain.Expressions
{
    public static class OrderExpression
    {
        public static Expression<Func<Order, bool>> Filter(string userId = null)
        {
            return x => string.IsNullOrEmpty(userId) || x.UserId == userId;
        }
        public static Func<Order, bool> PendingToNotify()
        {
            return x => x.OrderStatus != OrderStatus.PENDING && x.NotifiedAt == null ;
        }
    }
}
