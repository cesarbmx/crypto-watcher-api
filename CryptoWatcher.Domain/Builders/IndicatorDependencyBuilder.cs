using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class IndicatorDependencyBuilder
    {
        public static void BuildLevel(List<IndicatorDependency> dependencies)
        {
            // Build
            foreach (var dependency in dependencies)
            {
                // We pick its dependencies
                var findings = dependencies.Where(x => x.IndicatorId == dependency.DependsOn).ToList();
                // If it does have dependencies, we first set these (recursive)
                if (findings.Count > 0) BuildLevel(findings);
                // Once its dependencies have been set, we pick the max
                var level = findings.Select(x => x.Level).Max();
                // We set the level
                dependency.SetLevel(level + 1);
            }
        }
    }
}
