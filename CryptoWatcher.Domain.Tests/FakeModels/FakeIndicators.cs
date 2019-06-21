using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Tests.FakeModels
{
    public static class FakeIndicatorDependencies
    {
        public static List<IndicatorDependency> GetFakeIndicatorDependencies()
        {
            var time = DateTime.Now;
            var indicatorDependencies = new List<IndicatorDependency>()
            {
                new IndicatorDependency("price-change-24hrs", "price", time),
                new IndicatorDependency("hype", "price-change-24hrs", time),

            };
            return indicatorDependencies;
        }
    }
}
