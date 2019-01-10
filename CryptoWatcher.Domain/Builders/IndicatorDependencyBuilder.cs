using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class IndicatorDependencyBuilder
    {
        public static List<IndicatorDependency> BuildDependencies(string indicatorId, string[] dependencies)
        {
            var indicatorDependencies = new List<IndicatorDependency>();

            // Build
            foreach (var dependingIndicatorId in dependencies)
            {
                indicatorDependencies.Add(new IndicatorDependency(indicatorId, dependingIndicatorId));
            }

            // Return
            return indicatorDependencies;
        }
        public static void BuildLevel(List<IndicatorDependency> dependencies)
        {
            // Build
            foreach (var dependency in dependencies)
            {
                // If we have already set a value, we skip it
                if (dependency.Level.HasValue) continue;
                // We pick its dependencies
                var findings = dependencies.Where(x => x.IndicatorId == dependency.DependsOn).ToList();
                // If it does have dependencies, we first set these (recursive)
                if (findings.Count > 0) BuildLevel(findings);
                // Once its dependencies have been set, we pick the max
                var level = findings.Select(x => x.Level).Max() ?? 0;
                // We set the level
                dependency.SetLevel(level + 1);
            }
        }
    }
}
