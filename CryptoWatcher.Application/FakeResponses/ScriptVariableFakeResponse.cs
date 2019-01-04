using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class ScriptVariableFakeResponse
    {
        public static ScriptVariablesResponse GetFake_List()
        {
           var scriptVariablesResponse = new ScriptVariablesResponse();

           var now = DateTime.Now;
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

            scriptVariablesResponse.Times = times;
            scriptVariablesResponse.Currencies = currencies;
            scriptVariablesResponse.Indicators = indicators;
            scriptVariablesResponse.Values = level1;

            // Return
            return scriptVariablesResponse;
        }
    }
}
