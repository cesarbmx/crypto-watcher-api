using System.Collections.Generic;
using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Cache: Entity
    {
        public string Value { get; private set; }

        public Cache() : base(null)
        {}

        public List<T> Get<T>(CacheKey key)
        {
            return JsonConvertHelper.DeserializeObjectRaw<List<T>>(Value);
        }
        public Cache Set<T>(CacheKey key, List<T> value)
        {
            Id = key.ToString();
            Value = JsonConvertHelper.SerializeObjectRaw(value);

            return this;
        }
    }
}
