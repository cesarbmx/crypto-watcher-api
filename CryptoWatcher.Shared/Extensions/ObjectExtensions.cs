using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CryptoWatcher.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, object> AsDictionary(this object obj, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            var dictionary = obj.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(obj, null)
            );
            var result = new Dictionary<string, object>();
            
            foreach (var item in dictionary)
            {
                var fullName = item.Value?.GetType().FullName;
                if (fullName != null && !fullName.StartsWith("System.") && !(item.Value is Enum))
                {
                    result.Add(item.Key, item.Value.AsDictionary());
                }
                else
                {
                    if (item.Value is Array array)
                    {
                        result.Add(item.Key, $"[{string.Join(",", (string[])array)}]");
                    }
                    else
                    {
                        result.Add(item.Key, item.Value);
                    }
                }
            }
            return result;
        }
    }
}
