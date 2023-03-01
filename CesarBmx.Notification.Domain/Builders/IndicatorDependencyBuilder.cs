using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.Notification.Domain.Models;


namespace CesarBmx.Notification.Domain.Builders
{
    public static class IndicatorDependencyBuilder
    {
        public static List<IndicatorDependency> BuildIndicatorDependencies(string indicatorId, List<Indicator> dependencies)
        {
            // Now
            var now = DateTime.UtcNow.StripSeconds();

            // Prepare list
            var indicatorDependencies = new List<IndicatorDependency>();

            // Build
            foreach (var dependency in dependencies)
            {
                // Create
                var indicatorDependency = new IndicatorDependency(indicatorId, dependency.IndicatorId, now);

                // Add
                indicatorDependencies.Add(indicatorDependency);
            }

            // Return
            return indicatorDependencies;
        }


       
    }
}
