using System;
using System.Collections.Generic;
using CesarBmx.Shared.Domain.Models;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Domain.Models
{
    public class Indicator : IEntity
    {
        public string Id => IndicatorId;
        public string IndicatorId { get; private set; }
        public IndicatorType IndicatorType { get; private set; }
        public string UserId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Formula { get; private set; }
        public List<IndicatorDependency> Dependencies { get; private set; }
        public int DependencyLevel { get; private set; }
        public DateTime Time { get; private set; }

        public Indicator() { }
        public Indicator(
            string indicatorId,
            IndicatorType indicatorType,
            string userId,
            string name, 
            string description,
            string formula,
            List<IndicatorDependency> dependencies,
            int dependencyLevel,
            DateTime time)
        {
            IndicatorId = indicatorId;
            IndicatorType = indicatorType;
            UserId = userId;
            Name = name;
            Description = description;
            Formula = formula;
            Dependencies = dependencies;
            DependencyLevel = dependencyLevel;
            Time = time;
        }

        public Indicator SetDependencies(List<IndicatorDependency> dependencies)
        {
            Dependencies = dependencies;

            return this;
        }
        public Indicator SetDependencyLevel(int dependencyLevel)
        {
            DependencyLevel = dependencyLevel;

            return this;
        }
        public Indicator Update(string name, string description, string formula)
        {
            Name = name;
            Description = description;
            Formula = formula;

            return this;
        }
    }
}
