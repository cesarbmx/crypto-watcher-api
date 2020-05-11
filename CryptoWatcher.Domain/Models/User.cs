using System;
using CesarBmx.Shared.Domain.Models;


namespace CryptoWatcher.Domain.Models
{
    public class User : IEntity
    {
        public string Id => UserId;
        public string UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User() { }
        public User(string userId, DateTime time)
        {
            UserId = userId;
            CreatedAt = time;
        }
    }
}
