using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hyper.Domain.Services
{
    public class ErrorMessagesService
    {
        public Dictionary<string, IEnumerable<string>> GetAllErrorMessages()
        {
            var errorMessages = new Dictionary<string, IEnumerable<string>>();

            var query = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.Namespace == "Hyper.Domain.Messages"
                    select t;
            var types = query.ToList();

            foreach (var type in types)
            {
               var constants = type.GetFields(BindingFlags.Public | BindingFlags.Static |
                                              BindingFlags.FlattenHierarchy)
                   .Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
          
                var y = new List<string>();

                foreach (var constant in constants)
                {                  
                    y.Add(constant.GetValue(null).ToString());
                }
                var lenght = type.Name.Length - 8;
                errorMessages.Add(type.Name.Remove(lenght), y);
            }

            return errorMessages;
        }

    }
}
