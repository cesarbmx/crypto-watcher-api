using System.Collections.Generic;
using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Cache: Entity
    {
        public string Value { get; private set; }

        public List<T> GetValue<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<List<T>>(Value);
        }
        public Cache SetValue<T>(List<T> value)
        {
            Id = typeof(T).Name;
            Value = JsonConvertHelper.SerializeObjectRaw(value);

            return this;
        }
        public Cache SetValue<T>(List<T> value, string key)
        {
            Id = key;
            Value = JsonConvertHelper.SerializeObjectRaw(value);

            return this;
        }
        public Cache SetValue<T>(T value)
        {
            Id = typeof(T).Name;
            Value = JsonConvertHelper.SerializeObjectRaw(value);

            return this;
        }
        public Cache SetValue<T>(T value, string key)
        {
            Id = key;
            Value = JsonConvertHelper.SerializeObjectRaw(value);

            return this;
        }
    }
}
