using System;
using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Log : Entity
    {
        public string LogId => Id;
        public string Action { get; private set; }
        public string Entity { get; private set; }
        public string EntityId { get; private set; }
        public string Json { get; private set; }

        public Log() { }
        public Log(string action, object entity, string entityId, string createdBy, DateTime creationTime)
        : base(createdBy)
        {
            var entityName = entity.GetType().Name;
            if (entityName == "List`1")
            {
                entityName = entity.GetType().GetGenericArguments()[0].Name + "List";
            }

            Action = action;
            Entity = entityName;
            EntityId = entityId;
            CreatedBy = createdBy;
            CreationTime = creationTime;
            Json = JsonConvertHelper.SerializeObjectRaw(entity);
        }
        public T ModelJsonToObject<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<T>(Json);
        }
    }
}
