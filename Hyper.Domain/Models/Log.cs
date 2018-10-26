using System;
using Hyper.Shared.Helpers;

namespace Hyper.Domain.Models
{
    public class Log
    {
        public Guid Id { get; private set; }
        public string ModelName { get; private set; }
        public string ActionName { get; private set; }
        public string ModelJson { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Log() { }
        public Log(string modelName, string actionName, object model)
        {
            Id = Guid.NewGuid();
            ModelName = modelName;
            ActionName = actionName;
            ModelJson = JsonConvertHelper.SerializeObjectRaw(model);
            CreationTime = DateTime.Now;
        }
        public T ModelJsonToObject<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<T>(ModelJson);
        }
    }
}
