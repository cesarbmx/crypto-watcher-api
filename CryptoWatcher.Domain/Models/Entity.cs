using System;

namespace CryptoWatcher.Domain.Models
{
    public abstract class Entity
    {
        public string Id { get; protected set; }
        //public User CreatedBy { get; set; }
        public DateTime CreationTime { get; protected set; }
        //public User LastModifiedBy { get; set; }
        //public DateTime LastModificationTime { get; set; }

        protected Entity()
        {
            CreationTime = DateTime.Now;
        }
    }
}
