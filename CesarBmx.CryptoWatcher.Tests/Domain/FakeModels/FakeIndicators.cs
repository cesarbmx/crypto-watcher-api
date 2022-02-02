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
                new IndicatorDependency("master.PRICE_CHANGE_24H","master.PRICE", DateTime.UtcNow.StripSeconds()),
                new IndicatorDependency("master.HYPE","master.PRICE_CHANGE_24H", DateTime.UtcNow.StripSeconds()),

            };
            return indicatorDependencies;
        }
    }
}
