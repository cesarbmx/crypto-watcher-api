using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Log : Entity
    {
        public string LogId => Id;
        public string Resource { get; private set; }
        public string Action { get; private set; }
        public string Json { get; private set; }

        public Log() { }
        public Log(object resource, string action)
        {
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
