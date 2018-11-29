using System;


namespace CryptoWatcher.Domain.Models
{
    public class Notification : Entity
    {
        public string PhoneNumber { get; private set; }
        public string Message { get; private set; }
        public DateTime? WhatsappSentTime { get; private set; }

        public Notification() { }
        public Notification(string phoneNumber, string message)
        {
            Id = Guid.NewGuid().ToString();
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
