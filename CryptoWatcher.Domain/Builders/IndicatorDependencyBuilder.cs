using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class IndicatorDependencyBuilder
    {
        public static List<IndicatorDependency> BuildIndicatorDependencies(string indicatorId, List<Indicator> dependencies, DateTime time)
        {
            // New up list
            var indcatorDependencies = new List<IndicatorDependency>();

            // For each dependency
            foreach (var dependency in dependencies)
            {
                // Create
                var indicatorDependency = new IndicatorDependency(indicatorId, dependency.IndicatorId, time);

                // Add
                indcatorDependencies.Add(indicatorDependency);
            }

            // Return
            return indcatorDependencies;
        }
    }
}
