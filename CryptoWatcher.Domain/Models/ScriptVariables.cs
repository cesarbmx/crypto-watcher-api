using System;
using System.Collections.Generic;


namespace CryptoWatcher.Domain.Models
{
    public class ScriptVariables
    {
        public DateTime[] Times { get; private set; }
        public string[] Currencies { get; private set; }
        public string[] Indicators { get; private set; }
        public Dictionary<DateTime, Dictionary<IndicatorType, Dictionary<string, Dictionary<string, decimal>>>> Values { get; private set; }

        public ScriptVariables() { }
        public ScriptVariables(
            DateTime[] times,
            string[] currencies,
            string[] indicators,
            Dictionary<DateTime, Dictionary<IndicatorType, Dictionary<string, Dictionary<string, decimal>>>> values)
        {
            Times = times;
            Currencies = currencies;
            Indicators = indicators;
            Values = values;
        }
    }
}
