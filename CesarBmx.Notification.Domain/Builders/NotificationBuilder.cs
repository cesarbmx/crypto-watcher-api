using System;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.Notification.Domain.Types;


namespace CesarBmx.Notification.Domain.Builders
{
    public static class NotificationBuilder
    {
        public static NotificationStatus BuildNotificationStatus(DateTime? sentTime)
        {
            // If sent already, Notified
            if (sentTime.HasValue) return NotificationStatus.NOTIFIED;

            // If not sent yet, Pending
            return NotificationStatus.PENDING;
        }
        public static string BuildMessage(string messageTemplate, string currencyId, OrderType orderType, decimal price)
        {
            // Create message
            var message = string.Format(
                messageTemplate,
                currencyId.ToUpper(),
                orderType.ToString().ToLower(),
                price.Normalize());

            // Return
            return message;
        }
    }
}
