using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CryptoWatcher.Domain.Services
{
    public class ErrorMessagesService
    {
        public Dictionary<string, Dictionary<string, string>> GetErrorMessages()
        {
            var resources = new Dictionary<string, Dictionary<string, string>>();

            var query = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.IsClass && t.Namespace == "CryptoWatcher.Domain.Messages"
                        select t;
            var types = query.ToList();

            foreach (var type in types)
            {
                var constants = type.GetFields(BindingFlags.Public | BindingFlags.Static |
                                               BindingFlags.FlattenHierarchy)
                    .Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();

                var errorMessages = new Dictionary<string, string>();

                foreach (var constant in constants)
                {
                    errorMessages.Add(constant.Name, constant.GetValue(null).ToString());
                }
                resources.Add(type.Name.Replace("Messages", ""), errorMessages);
            }

            // Append common messages to each resource
            var messages = resources.FirstOrDefault(x => x.Key == "").Value;
            resources.Remove("");
            foreach (var resource in resources)
            {
                foreach (var message in messages)
                {
                    if (!resource.Value.ContainsKey(message.Key))
                    {
                        resource.Value.Add(message.Key, message.Value);
                    }
                }
            }



            return resources;
        }

    }
}
