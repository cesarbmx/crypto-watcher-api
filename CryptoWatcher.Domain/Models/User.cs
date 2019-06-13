using System;


namespace CryptoWatcher.Domain.Models
{
    public class User : IEntity
    {
        public string Id => UserId;
        public string UserId { get; private set; }
        public DateTime Time { get; private set; }

        public User() { }
        public User(string userId, DateTime time)
        {
            UserId = userId;
            Time = time;
        }
    }
}
