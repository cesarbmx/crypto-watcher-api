using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Tests.FakeModels
{
    public class FakeScriptVariables : ScriptVariables
    {
        public FakeScriptVariables()
        {
            var now = DateTime.Now;
            Times = new [] { now };
            Currencies = new [] { "bitcoin", "ethereum", "ripple", "bitcoin-cash", "eos" };
            Values = new Dictionary<DateTime, Dictionary<string, Dictionary<string, decimal?>>>()
            {
                {
                    now, new Dictionary<string, Dictionary<string, decimal?>>
                    {
                        {
                            "price", new Dictionary<string, decimal?>
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
