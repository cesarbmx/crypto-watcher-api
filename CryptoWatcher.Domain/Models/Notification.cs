using System;


namespace CryptoWatcher.Domain.Models
{
    public class Notification : IEntity
    {
        public string Id => NotificationId.ToString();
        public Guid NotificationId { get; private set; }
        public string UserId { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Message { get; private set; }
        public DateTime? WhatsappSentTime { get; private set; }
        public DateTime Time { get; private set; }

        public Notification() { }
        public Notification(string userId, string phoneNumber, string message)
        {
            NotificationId = Guid.NewGuid();
            UserId = userId;
            PhoneNumber = phoneNumber;
            Message = message;
            WhatsappSentTime = null;
            Time = DateTime.Now;
        }

        public void SendWhatsapp()
        {
            WhatsappSentTime = DateTime.Now;
        }
    }
}
