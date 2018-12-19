using System;


namespace CryptoWatcher.Domain.Models
{
    public class Notification : IEntity
    {
        public string Id => NotificationId;
        public string NotificationId { get; private set; }
        public string UserId { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Message { get; private set; }
        public DateTime? WhatsappSentTime { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Notification() { }
        public Notification(string userId, string phoneNumber, string message)
        {
            NotificationId = Guid.NewGuid().ToString();
            UserId = userId;
            PhoneNumber = phoneNumber;
            Message = message;
            WhatsappSentTime = null;
            CreatedBy = userId;
            CreationTime = DateTime.Now;
        }

        public void SendWhatsapp()
        {
            WhatsappSentTime = DateTime.Now;
        }
    }
}
