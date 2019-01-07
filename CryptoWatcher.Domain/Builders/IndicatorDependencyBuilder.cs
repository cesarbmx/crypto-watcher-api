using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class IndicatorDependencyBuilder
    {
        public static void BuildDependencyLevel(List<IndicatorDependency> dependencies)
        {
            // Build
            foreach (var dependency in dependencies)
            {
                // If we have already set a value, we skip it
                if (dependency.Level.HasValue) continue;
                // We pick its dependencies
                var findings = dependencies.Where(x => x.IndicatorId == dependency.DependsOn).ToList();
                // If it does have dependecnies, we first set these (recursive)
                if (findings.Count > 0) BuildDependencyLevel(findings);
                // Once its dependencies have been set, we pick the max
                var level = findings.Select(x => x.Level).Max() ?? 0;
                // We set the level
                dependency.SetLevel(level + 1);
            }
        }
    }
}
