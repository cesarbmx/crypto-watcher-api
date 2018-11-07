using System.Collections.Generic;
using Hyper.Shared.Helpers;

namespace Hyper.Domain.Models
{
    public class Cache: Entity
    {
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
