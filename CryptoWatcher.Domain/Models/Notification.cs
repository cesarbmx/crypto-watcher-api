using System;
using CesarBmx.Shared.Domain.Entities;
using CryptoWatcher.Domain.ModelBuilders;
using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Domain.Models
{
    public class Notification : IEntity
    {
        public string Id => NotificationId.ToString();

        public int NotificationId { get; private set; }
        public string UserId { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Message { get; private set; }
        public DateTime? SentTime { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public NotificationStatus NotificationStatus => NotificationBuilder.BuildNotificationStatus(SentTime);

        public Notification() { }
        public Notification(string userId, string phoneNumber, string message, DateTime time)
        {
            NotificationId = 0;
            UserId = userId;
            PhoneNumber = phoneNumber;
            Message = message;
            SentTime = null;
            CreatedAt = time;
        }

        public void MarkAsSent()
        {
            SentTime = DateTime.Now;
        }
    }
}
