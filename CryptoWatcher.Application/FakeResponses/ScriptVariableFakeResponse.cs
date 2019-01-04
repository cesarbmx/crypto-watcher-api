using System.Collections.Generic;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class ScriptVariableFakeResponse
    {
        public static Dictionary<string, Dictionary<string, Dictionary<string, decimal>>> GetFake_List()
        {
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
            var level21 = new Dictionary<string, Dictionary<string, decimal>>
            {
                { "bitcoin", level31 },
                { "ethereum", level32 }
            };
            var level22 = new Dictionary<string, Dictionary<string, decimal>>
            {
                { "master", level33 }
            };
            var level1 = new Dictionary<string, Dictionary<string, Dictionary<string, decimal>>>
            {
                { "CurrencyIndicator", level21 },
                { "UserIndicator", level22 }
            };

            // Return
            return level1;
        }
    }
}
