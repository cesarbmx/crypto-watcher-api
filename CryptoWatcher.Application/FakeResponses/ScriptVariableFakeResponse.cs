using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class ScriptVariableFakeResponse
    {
        public static Dictionary<DateTime, Dictionary<IndicatorType, Dictionary<string, Dictionary<string, decimal>>>> GetFake_List()
        {
            var level41 = new Dictionary<string, decimal>
            {
                { "price", 3200m },
                { "hype", 0.125m }
            };
            var level42 = new Dictionary<string, decimal>
            {
                { "price", 570m },
                { "hype", 6.5m }
            };
            var level43 = new Dictionary<string, decimal>
            {
                { "performance", 70m }
            };
            var level31 = new Dictionary<string, Dictionary<string, decimal>>
            {
                { "bitcoin", level41 },
                { "ethereum", level42 }
            };
            var level32 = new Dictionary<string, Dictionary<string, decimal>>
            {
                { "master", level43 }
            };
            var level2 = new Dictionary<IndicatorType, Dictionary<string, Dictionary<string, decimal>>>
            {
                { IndicatorType.CurrencyIndicator, level31 },
                { IndicatorType.UserIndicator, level32 }
            };
            var level1 = new Dictionary<DateTime, Dictionary<IndicatorType, Dictionary<string, Dictionary<string, decimal>>>>
            {
                {DateTime.Now, level2}
            };

            // Return
            return level1;
        }
    }
}
