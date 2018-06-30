using System;
using System.Collections.Generic;
using Hyper.Shared.Helpers;

namespace Hyper.Domain.Models
{
    public class Cache
    {
        public string Key { get; private set; }
       
        public string Value { get; private set; }
        public DateTime CreationTime { get; private set; }


        public IEnumerable<T> GetValue<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<IEnumerable<T>>(Value);
        }
        public void SetValue<T>(string key, IEnumerable<T> value)
        {
            Key = key;
            Value = JsonConvertHelper.SerializeObjectRaw(value);
            CreationTime = DateTime.Now;
        }
    }
}
