using System;


namespace CryptoWatcher.Domain.Models
{
    public class Notification : Entity
    {
        public string NotificationId { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Message { get; private set; }
        public bool WhatsappSent { get; private set; }
        public DateTime? WhatsappSentTime { get; private set; }

        public Notification() { }
        public Notification(string phoneNumber, string message)
        {
            NotificationId = Id;
            PhoneNumber = phoneNumber;
            Message = message;
            WhatsappSent = false;
            WhatsappSentTime = null;
        }
    }
}
