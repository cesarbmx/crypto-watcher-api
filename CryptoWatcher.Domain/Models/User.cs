using System;
using System.Collections.Generic;
using CesarBmx.Shared.Domain.Models;


namespace CryptoWatcher.Domain.Models
{
    public class User : IEntity
    {
        public string Id => UserId;
        public string UserId { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public List<Watcher> Watchers { get; private set; }
        public List<Order> Orders { get; private set; }
        public List<Notification> Notifications { get; private set; }

        public User() { }
        public User(string userId, string phoneNumber, DateTime createdAt)
        {
            UserId = userId;
            PhoneNumber = phoneNumber;
            CreatedAt = createdAt;
            Notifications = new List<Notification>();
            Orders = new List<Order>();
            Watchers = new List<Watcher>();
        }
    }
}
