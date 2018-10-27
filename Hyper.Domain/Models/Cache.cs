using System;
using System.Collections.Generic;
using Hyper.Shared.Helpers;

namespace Hyper.Domain.Models
{
    public class Cache : IEntity
    {
        public int Id { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public DateTime CreationTime { get; private set; }

        public List<T> GetValue<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<List<T>>(Value);
        }
        public void SetValue<T>(List<T> value)
        {
            Id = 0;
            Key = typeof(T).Name;
            Value = JsonConvertHelper.SerializeObjectRaw(value);
            CreationTime = DateTime.Now;
        }
    }
}
