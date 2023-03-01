using System;


namespace CesarBmx.Notification.Domain.Models
{
    public class User
    {
        public string UserId { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User() { }
        public User(string userId, string phoneNumber, DateTime createdAt)
        {
            UserId = userId;
            PhoneNumber = phoneNumber;
            CreatedAt = createdAt;
        }
    }
}
