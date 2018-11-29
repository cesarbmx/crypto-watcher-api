using System.Collections.Generic;
using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Cache: Entity
    {
        public string Key => Id;
        public string Value { get; private set; }

        public List<T> GetValue<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<List<T>>(Value);
        }
        public void SetValue<T>(List<T> value)
        {
            Id = typeof(T).Name;
            Value = JsonConvertHelper.SerializeObjectRaw(value);
        }
    }
}
