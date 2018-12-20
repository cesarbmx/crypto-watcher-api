
using System;


namespace CryptoWatcher.Shared.Models
{
    public interface IEntity
    {
        string Id { get;}
        string CreatedBy { get; }
        DateTime Time { get; }
    }
}
