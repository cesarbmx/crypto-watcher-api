using System;
using System.Linq.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Domain.Expressions
{
    public static class NotificationExpression
    {
        public static Expression<Func<Notification, bool>> Filter(string userId = null)
        {
            return x => string.IsNullOrEmpty(userId) || x.UserId == userId;
        }
        public static Expression<Func<Notification, bool>> PendingNotification()
        {
            return x => !x.SentTime.HasValue;
        }
    }
}
