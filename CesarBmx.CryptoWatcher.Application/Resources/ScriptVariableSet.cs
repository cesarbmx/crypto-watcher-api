using System;
using System.Collections.Generic;

namespace CesarBmx.CryptoWatcher.Application.Resources
{
    public class ScriptVariableSet
    {
        public List<DateTime> Times { get; set; }
        public List<string> Currencies { get; set; }
        public List<string> Indicators { get; set; }
        public Dictionary<DateTime, Dictionary<string, Dictionary<string, decimal>>> Values { get; set; }
    }
}
