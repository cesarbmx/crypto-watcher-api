using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Tests.Domain.FakeModels
{
    public static class FakeIndicatorDependencies
    {
        public static List<IndicatorDependency> GetFakeIndicatorDependencies()
        {
            var indicatorDependencies = new List<IndicatorDependency>()
            {
                new IndicatorDependency("price-change-24hrs", "price", DateTime.Now),
                new IndicatorDependency("hype", "price-change-24hrs", DateTime.Now),

            };
            return indicatorDependencies;
        }
    }
}
