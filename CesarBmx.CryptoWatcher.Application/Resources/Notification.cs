using System;
using CesarBmx.CryptoWatcher.Domain.Types;

namespace CesarBmx.CryptoWatcher.Application.Resources
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime? SentTime { get; set; }
        public DateTime Time { get; set; }
        public NotificationStatus NotificationStatus  { get; set; }
    }
}
