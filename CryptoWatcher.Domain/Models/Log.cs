using System;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Log : IEntity
    {
        public string Id { get; private set; }
        public string LogId => Id;
        public string Action { get; private set; }
        public string Entity { get; private set; }
        public string EntityId { get; private set; }
        public string Json { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Log() { }
        public Log(string action, object entity, string entityId, string createdBy, DateTime creationTime)
        {
            var entityName = entity.GetType().Name;
            if (entityName == "List`1")
            {
                entityName = entity.GetType().GetGenericArguments()[0].Name + "List";
            }

            Id = Guid.NewGuid().ToString();
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
