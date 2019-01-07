using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class NotificationExpression
    {
        public static Expression<Func<Notification, bool>> NotificationFilter(string userId = null)
        {
            return x => string.IsNullOrEmpty(userId) || x.UserId == userId;
        }
        public static Expression<Func<Notification, bool>> PendingNotification()
        {
            return x => !x.SentTime.HasValue;
        }
    }
}
