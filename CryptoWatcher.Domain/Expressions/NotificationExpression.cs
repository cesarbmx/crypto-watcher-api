using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class NotificationExpression
    {
        public static Expression<Func<Notification, bool>> Filter(string userId)
        {
            return x => x.UserId == userId;
        }
        public static Expression<Func<Notification, bool>> PendingNotification()
        {
            return x => !x.WhatsappSentTime.HasValue;
        }
    }
}
