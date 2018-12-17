using System;


namespace CryptoWatcher.Domain.Models
{
    public class Notification : Entity
    {
        public string NotificationId => Id;
        public string UserId { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Message { get; private set; }
        public DateTime? WhatsappSentTime { get; private set; }

        public Notification() { }
        public Notification(string userId, string phoneNumber, string message)
        : base(userId)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            PhoneNumber = phoneNumber;
            Message = message;
            WhatsappSentTime = null;
        }

        public void SendWhatsapp()
        {
            WhatsappSentTime = DateTime.Now;
        }
    }
}
