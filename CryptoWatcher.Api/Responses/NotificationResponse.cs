


using System;

namespace CryptoWatcher.Api.Responses
{
    public class NotificationResponse
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime? WhatsappSentTime { get; set; }
    }
}
