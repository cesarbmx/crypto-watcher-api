using System;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Domain.Expressions
{
    public static class OrderExpression
    {
        public static Func<Order, bool> PendingToNotify()
        {
            return x => x.OrderStatus != OrderStatus.PENDING && x.NotifiedAt == null ;
        }
    }
}
