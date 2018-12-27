using System;


namespace CryptoWatcher.Application.Notifications.Responses
{
    public class NotificationResponse
    {
        public Guid NotificationId { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime? WhatsappSentTime { get; set; }
    }
}
