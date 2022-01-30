using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Models;


namespace CesarBmx.CryptoWatcher.Tests.Domain.FakeModels
{
    public class FakeScriptVariables : ScriptVariables
    {
        public FakeScriptVariables()
        {
            var now = DateTime.UtcNow.StripSeconds();
            Times = new [] { now };
            Currencies = new [] { "bitcoin", "ethereum", "ripple", "bitcoin-cash", "eos" };
            Values = new Dictionary<DateTime, Dictionary<string, Dictionary<string, decimal>>>()
            {
                {
                    now, new Dictionary<string, Dictionary<string, decimal>>
                    {
                        {
                            "PRICE", new Dictionary<string, decimal>
                            {
                                {"bitcoin", 0.25m},
                                {"ethereum", 0.25m},
                                {"ripple", 0.25m},
                                {"bitcoin-cash", 0.25m},
                                {"eos", 0.25m},
                            }
                        }
                    }
                }
            };
        }
    }
}
