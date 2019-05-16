
using System;

namespace CryptoWatcher.Domain.Models
{
    public interface IEntity
    {
        string Id { get;}
        DateTime Time { get;}
    }
}
