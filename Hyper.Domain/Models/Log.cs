using Hyper.Shared.Helpers;

namespace Hyper.Domain.Models
{
    public class Log : Entity
    {
        public string Model { get; private set; }
        public string Action { get; private set; }
        public string Json { get; private set; }

        public Log() { }
        public Log(object model, string action)
        {
            Model = model.GetType().Name;
            Action = action;
            Json = JsonConvertHelper.SerializeObjectRaw(model);
        }
        public T ModelJsonToObject<T>()
        {
            return JsonConvertHelper.DeserializeObjectRaw<T>(Json);
        }
    }
}
