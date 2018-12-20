using System.Collections.Generic;

namespace CryptoWatcher.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// <para>Converts to clear key-value pairs in Splunk.</para>
        /// <seealso cref="http://dev.splunk.com/view/logging/SP-CAAAFCK"/>
        /// <para>Example: 2018-12-20 07:46:05.113 Level=INFO, App=MyApp, Environment=Development, Event=MainJob, ExecutionTime=0.5973352</para>
        /// </summary>
        /// <param name="dictionary">Property name and value</param>
        /// <param name="prefix">A prefix can be added. Although this parameter is used to build subclasses recursively</param>
        /// <returns>A string</returns>
        public static string AsSplunkKeyValueString(this Dictionary<string, object> dictionary, string prefix = null)
        {
            var str = string.Empty;
            foreach (var item in dictionary)
            {
                if (item.Value is Dictionary<string, object> obj)
                {
                    str += obj.AsSplunkKeyValueString(item.Key) + ", ";
                }
                else
                {
                    // We skip those properties with spaces
                    if (!item.Value.ToString().Contains(" "))
                    {
                        var pref = prefix?.Length > 0 ? prefix + "_" : string.Empty;
                        str += pref + item.Key + "=" + item.Value + ", ";
                    }
                }
            }
            return str.Length > 0 ? str.Substring(0, str.Length - 2) : str;
        }
    }
}