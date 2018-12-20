using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CryptoWatcher.Shared.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts a custom class (with its subclasses) to a dcitionary
        /// </summary>
        /// <param name="obj">Teh object to convert</param>
        /// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags"></see> that specify how the search is conducted.   -or-   Zero, to return null.</param>
        /// <returns>A string/value dictionary</returns>
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
                var fullName = item.Value.GetType().FullName;
                if (fullName != null && !fullName.StartsWith("System.") && !(item.Value is Enum))
                {
                    result.Add(item.Key, item.Value.AsDictionary());
                }
                else
                {
                    result.Add(item.Key,item.Value);
                }
            }
            return result;
        }
    }
}
