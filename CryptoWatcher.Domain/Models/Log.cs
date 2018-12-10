using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Log : Entity
    {
        public string LogId => Id;
        public string Entity { get; private set; }
        public string Action { get; private set; }
        public string EntityId { get; private set; }
        public string Json { get; private set; }

        public Log() { }
        public Log(object entity, string action, string entityId)
        {
            var entityName = entity.GetType().Name;
            if (entityName == "List`1")
            {
                entityName = entity.GetType().GetGenericArguments()[0].Name;
            }

            Entity = entityName;
            Action = action;
            EntityId = entityId;
            Json = JsonConvertHelper.SerializeObjectRaw(entity);
        }
        public T ModelJsonToObject<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<T>(Json);
        }
    }
}
