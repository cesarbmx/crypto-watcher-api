using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class ErrorMessageBuilder
    {
        public static Dictionary<string, ErrorMessage> BuildErrorMessages()
        {
            var resources = new Dictionary<string, ErrorMessage>();

            var query = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.Namespace == "CryptoWatcher.Application.Messages"
                select t;
            var types = query.ToList();

            foreach (var type in types)
            {
                var constants = type.GetFields(BindingFlags.Public | BindingFlags.Static |
                                               BindingFlags.FlattenHierarchy)
                    .Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();

                var errorMessage = new ErrorMessage();

                foreach (var constant in constants)
                {
                    errorMessage.Add(constant.Name, constant.GetValue(null).ToString());
                }
                resources.Add(type.Name.Replace("Message", ""), errorMessage);
            }

            return resources.OrderBy(x=>x.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
