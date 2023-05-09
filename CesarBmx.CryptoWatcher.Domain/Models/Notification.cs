using System;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Types;


namespace CesarBmx.CryptoWatcher.Domain.Models
{
    public class Notification 
    {
        public int NotificationId { get; private set; }
        public string UserId { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Text { get; private set; }
        public DateTime? SentTime { get; private set; }
        public DateTime Time { get; private set; }
        public NotificationStatus NotificationStatus => NotificationBuilder.BuildNotificationStatus(SentTime);

        public Notification() { }
        public Notification(string userId, string phoneNumber, string text, DateTime time)
        {
            NotificationId = 0;
            UserId = userId;
            PhoneNumber = phoneNumber;
            Text = text;
            SentTime = null;
            Time = time;
        }

        public void MarkAsSent()
        {
            SentTime = DateTime.UtcNow.StripSeconds();
        }
    }
}
