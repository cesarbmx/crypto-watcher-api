using System;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Domain.Types;

namespace CesarBmx.CryptoWatcher.Domain.Expressions
{
    public static class OrderExpression
    {
        public static Func<Order, bool> PendingToNotify()
        {
            return x => x.OrderStatus != OrderStatus.PENDING && x.NotifiedAt == null ;
        }
    }
}
