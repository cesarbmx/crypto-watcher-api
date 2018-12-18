using System;
using CryptoWatcher.Shared.Domain;

namespace CryptoWatcher.Domain.Models
{
    public abstract class Entity: IEntity
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationTime { get; set; }

        protected Entity()
        {
            CreationTime = DateTime.Now;
        }
    }
}
