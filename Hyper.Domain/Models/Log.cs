using System;
using Hyper.Shared.Helpers;

namespace Hyper.Domain.Models
{
    public class Log : IEntity
    {
        public int Id { get; private set; }
        public string ModelName { get; private set; }
        public string Action { get; private set; }
        public string ModelJson { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Log() { }
        public Log(string modelName, string action, object model)
        {
            Id = 0;
            ModelName = modelName;
            Action = action;
            ModelJson = JsonConvertHelper.SerializeObjectRaw(model);
            CreationTime = DateTime.Now;
        }
        public T ModelJsonToObject<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<T>(ModelJson);
        }
    }
}
