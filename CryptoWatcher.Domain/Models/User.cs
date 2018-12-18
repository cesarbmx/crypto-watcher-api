


using System;
using CryptoWatcher.Shared.Domain;

namespace CryptoWatcher.Domain.Models
{
    public class User : IEntity
    {
        public string Id { get; private set; }
        public string UserId => Id;
        public string CreatedBy { get; private set; }
        public DateTime CreationTime { get; private set; }

        public User() { }
        public User(string userId)
        {
            Id = userId;
            CreatedBy = userId;
            CreationTime = DateTime.Now;
        }
    }
}
