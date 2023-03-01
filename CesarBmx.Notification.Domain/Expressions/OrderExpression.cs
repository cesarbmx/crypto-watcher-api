using System;
using CesarBmx.Notification.Domain.Models;
using CesarBmx.Notification.Domain.Types;

namespace CesarBmx.Notification.Domain.Expressions
{
    public static class OrderExpression
    {
        public static Func<Order, bool> PendingToNotify()
        {
            return x => x.OrderStatus != OrderStatus.PENDING && x.NotifiedAt == null ;
        }
    }
}
