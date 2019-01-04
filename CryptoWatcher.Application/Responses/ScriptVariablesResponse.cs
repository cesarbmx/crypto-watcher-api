
using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Responses
{
    public class ScriptVariablesResponse
    {
        public DateTime[] Times { get; set; }
        public string[] Currencies { get; set; }
        public string[] Indicators { get; set; }
        public Dictionary<DateTime, Dictionary<IndicatorType, Dictionary<string, Dictionary<string, decimal>>>> Values { get; set; }
    }
}
