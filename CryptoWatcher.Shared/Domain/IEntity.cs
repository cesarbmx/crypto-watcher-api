
using System;


namespace CryptoWatcher.Shared.Domain
{
    public interface IEntity
    {
        string Id { get;}
        string CreatedBy { get; }
        DateTime CreationTime { get; }
    }
}
