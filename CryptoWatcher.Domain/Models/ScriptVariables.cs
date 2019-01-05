using System;
using System.Collections.Generic;


namespace CryptoWatcher.Domain.Models
{
    public class ScriptVariables
    {
        public DateTime[] Times { get; protected set; }
        public string[] Currencies { get; protected set; }
        public string[] Indicators { get; protected set; }
        public Dictionary<DateTime, Dictionary<string, Dictionary<string, decimal?>>> Values { get; protected set; }

        public ScriptVariables() { }
        public ScriptVariables(
            DateTime[] times,
            string[] currencies,
            string[] indicators,
            Dictionary<DateTime, Dictionary<string, Dictionary<string, decimal?>>> values)
        {
            Times = times;
            Currencies = currencies;
            Indicators = indicators;
            Values = values;
        }
    }
}
