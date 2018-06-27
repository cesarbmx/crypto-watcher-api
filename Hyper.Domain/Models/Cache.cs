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


        public List<T> GetValue<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<List<T>>(Value);
        }
        public void SetValue<T>(string key, List<T> value)
        {
            Key = key;
            Value = JsonConvertHelper.SerializeObjectRaw(value);
            CreationTime = DateTime.Now;
        }
    }
}
