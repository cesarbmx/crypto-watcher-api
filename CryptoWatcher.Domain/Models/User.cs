using System;


namespace CryptoWatcher.Domain.Models
{
    public class User : IEntity
    {
        public string Id => UserId;
        public string UserId { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreationTime { get; private set; }

        public User() { }
        public User(string userId)
        {
            UserId = userId;
            CreatedBy = userId;
            CreationTime = DateTime.Now;
        }
    }
}
