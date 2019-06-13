using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class IndicatorDependencyBuilder
    {
        public static List<IndicatorDependency> BuildIndicatorDependencies(string indicatorId, List<Indicator> dependencies, DateTime time)
        {
            // Prepare list
            var indicatorDependencies = new List<IndicatorDependency>();

            // Build
            foreach (var dependency in dependencies)
            {
                // Create
                var indicatorDependency = new IndicatorDependency(indicatorId, dependency.IndicatorId, time);

                // Add
                indicatorDependencies.Add(indicatorDependency);
            }

            // Return
            return indicatorDependencies;
        }


       
    }
}
