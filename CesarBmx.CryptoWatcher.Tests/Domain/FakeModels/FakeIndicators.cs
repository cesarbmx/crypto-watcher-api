using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Models;


namespace CesarBmx.CryptoWatcher.Tests.Domain.FakeModels
{
    public static class FakeIndicatorDependencies
    {
        public static List<IndicatorDependency> GetFakeIndicatorDependencies()
        {
            var indicatorDependencies = new List<IndicatorDependency>()
            {
                new IndicatorDependency("master", "price-change-24hrs","master", "price", DateTime.UtcNow.StripSeconds()),
                new IndicatorDependency("master", "hype","master", "price-change-24hrs", DateTime.UtcNow.StripSeconds()),

            };
            return indicatorDependencies;
        }
    }
}
