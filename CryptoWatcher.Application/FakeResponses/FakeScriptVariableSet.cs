using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CryptoWatcher.Application.Responses;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class FakeScriptVariableSet
    {
        public static ScriptVariableSet GetFake_List()
        {
           var scriptVariableSetResponse = new ScriptVariableSet();

           var now = DateTime.UtcNow.StripSeconds();
           var times = new [] { now};
           var currencies = new [] { "bitcoin", "ethereum", "master" };
           var indicators = new [] { "price", "hype", "performance" };

            var level31 = new Dictionary<string, decimal>
            {
                { "price", 3200m },
                { "hype", 0.125m }
            };
            var level32 = new Dictionary<string, decimal>
            {
                { "price", 570m },
                { "hype", 6.5m }
            };
            var level33 = new Dictionary<string, decimal>
            {
                { "performance", 70m }
            };
            var level2 = new Dictionary<string, Dictionary<string, decimal>>
            {
                { "bitcoin", level31 },
                { "ethereum", level32 },
                { "master", level33 }
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
