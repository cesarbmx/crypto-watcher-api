﻿using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class IndicatorDependencyBuilder
    {
        public static List<IndicatorDependency> BuildIndicatorDependencies(string userId, string indicatorId, List<Indicator> dependencies)
        {
            // Now
            var now = DateTime.Now;

            // Prepare list
            var indicatorDependencies = new List<IndicatorDependency>();

            // Build
            foreach (var dependency in dependencies)
            {
                // Create
                var indicatorDependency = new IndicatorDependency(userId, indicatorId, dependency.UserId, dependency.IndicatorId, now);

                // Add
                indicatorDependencies.Add(indicatorDependency);
            }

            // Return
            return indicatorDependencies;
        }


       
    }
}
