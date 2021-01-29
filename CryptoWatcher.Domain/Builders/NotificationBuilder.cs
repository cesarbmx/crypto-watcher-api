using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Domain.Builders
{
    public static class NotificationBuilder
    {
        public static NotificationStatus BuildNotificationStatus(DateTime? sentTime)
        {
            // If sent already, Notified
            if (sentTime.HasValue) return NotificationStatus.Notified;

            // If not sent yet, Pending
            return NotificationStatus.Pending;
        }
    }
}
