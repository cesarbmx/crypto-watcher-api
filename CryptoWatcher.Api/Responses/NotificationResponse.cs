


using System;

namespace CryptoWatcher.Api.Responses
{
    public class NotificationResponse
    {
        public string NotificationId { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public bool WhatsappSent { get; set; }
        public DateTime? WhatsappSentTime { get; set; }
    }
}
