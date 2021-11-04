using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeScriptVariableSet
    {
        public static ScriptVariableSet GetFake_List()
        {
           var scriptVariableSetResponse = new ScriptVariableSet();

           var now = DateTime.UtcNow.StripSeconds();
           var times = new List<DateTime> { now};
           var currencies = new List<string> { "BTC", "ETH", "EOS" };
           var indicators = new List<string> { "Master.PRICE", "Master.HYPE" };

            var level31 = new Dictionary<string, decimal>
            {
                { "Master.PRICE", 3200m },
                { "Master.HYPE", 0.125m }
            };
            var level32 = new Dictionary<string, decimal>
            {
                { "Master.PRICE", 570m },
                { "Master.HYPE", 6.5m }
            };
            var level33 = new Dictionary<string, decimal>
            {
                { "Master.PRICE", 3m },
                { "Master.HYPE", 0.5m }
            };
            var level2 = new Dictionary<string, Dictionary<string, decimal>>
            {
                { "BTC", level31 },
                { "ETH", level32 },
                { "EOS", level33 }
            };
            var level1 = new Dictionary<DateTime, Dictionary<string, Dictionary<string, decimal>>>
            {
                {now, level2}
            };

            scriptVariableSetResponse.Times = times;
            scriptVariableSetResponse.Currencies = currencies;
            scriptVariableSetResponse.Indicators = indicators;
            scriptVariableSetResponse.Values = level1;

            // Return
            return scriptVariableSetResponse;
        }
    }
}
