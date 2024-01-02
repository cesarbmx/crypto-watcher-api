using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeScriptVariables
    {
        public static ScriptVariablesResponse GetFake_List()
        {
           var scriptVariablesResponse = new ScriptVariablesResponse();

           var now = DateTime.UtcNow.StripSeconds();
           var times = new List<DateTime> { now};
           var currencies = new List<string> { "BTC", "ETH", "EOS" };
           var indicators = new List<string> { "master.PRICE", "master.HYPE" };

            var level31 = new Dictionary<string, decimal>
            {
                { "master.PRICE", 3200m },
                { "master.HYPE", 0.125m }
            };
            var level32 = new Dictionary<string, decimal>
            {
                { "master.PRICE", 570m },
                { "master.HYPE", 6.5m }
            };
            var level33 = new Dictionary<string, decimal>
            {
                { "master.PRICE", 3m },
                { "master.HYPE", 0.5m }
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

            scriptVariablesResponse.Times = times;
            scriptVariablesResponse.Currencies = currencies;
            scriptVariablesResponse.Indicators = indicators;
            scriptVariablesResponse.Values = level1;

            // Return
            return scriptVariablesResponse;
        }
    }
}
