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
                new IndicatorDependency("Master.PRICE_CHANGE_24hrs","Master.PRICE", DateTime.UtcNow.StripSeconds()),
                new IndicatorDependency("Master.HYPE","Master.PRICE_CHANGE_24hrs", DateTime.UtcNow.StripSeconds()),

            };
            return indicatorDependencies;
        }
    }
}
