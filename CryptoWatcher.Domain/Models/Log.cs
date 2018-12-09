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
            var resourceName = resource.GetType().Name;
            if (resourceName == "List`1")
            {
                resourceName = resource.GetType().GetGenericArguments()[0].Name + "List";
            }

            Resource = resourceName;
            Action = action;
            Json = JsonConvertHelper.SerializeObjectRaw(resource);
        }
        public T ModelJsonToObject<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<T>(Json);
        }
    }
}
