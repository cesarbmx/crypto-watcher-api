using System;
using CryptoWatcher.Shared.Domain;

namespace CryptoWatcher.Domain.Models
{
    public abstract class Entity: IEntity
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationTime { get; set; }
        //public User LastModifiedBy { get; set; }
        //public DateTime LastModificationTime { get; set; }

        protected Entity() { }
        protected Entity(string createdBy)
        {
            CreatedBy = createdBy;
            CreationTime = DateTime.Now;
        }
    }
}
