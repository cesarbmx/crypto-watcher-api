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
                new IndicatorDependency("master", "price-change-24hrs","master", "price", DateTime.Now),
                new IndicatorDependency("master", "hype","master", "price-change-24hrs", DateTime.Now),

            };
            return indicatorDependencies;
        }
    }
}
