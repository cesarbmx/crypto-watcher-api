
using System;


namespace CryptoWatcher.Domain.Models
{
    public interface IEntity
    {
        string Id { get;}
        string CreatedBy { get; }
        DateTime CreationTime { get; }
    }
}
