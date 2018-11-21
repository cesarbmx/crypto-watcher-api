using System;
using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Log : Entity
    {
        public string Resource { get; private set; }
        public string Action { get; private set; }
        public string Json { get; private set; }

        public Log() { }
        public Log(object resource, string action)
        {
            Id = Guid.NewGuid().ToString();
            Resource = resource.GetType().Name;
            Action = action;
            Json = JsonConvertHelper.SerializeObjectRaw(resource);
        }
        public T ModelJsonToObject<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<T>(Json);
        }
    }
}
