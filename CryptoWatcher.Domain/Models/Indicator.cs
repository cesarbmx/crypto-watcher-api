using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Builders;

namespace CryptoWatcher.Domain.Models
{
    public class Indicator : IEntity
    {
        public string Id => IndicatorId;
        public IndicatorType IndicatorType { get; private set; }
        public string IndicatorId { get; private set; }
        public string UserId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Formula { get; private set; }
        public List<IndicatorDependency> Dependencies { get; private set; }
        public int DependencyLevel => IndicatorBuilder.BuildDependencyLevel(Dependencies);
        public DateTime Time { get; private set; }

        public Indicator() { }
        public Indicator(
            IndicatorType indicatorType,
            string indicatorId,
            string userId,
            string name, 
            string description,
            string formula,
            List<IndicatorDependency> dependencies)
        {
            IndicatorType = indicatorType;
            IndicatorId = indicatorId;
            UserId = userId;
            Name = name;
            Description = description;
            Formula = formula;
            Dependencies = dependencies;
            Time = DateTime.Now;
        }

        public Indicator SetDependencies(List<IndicatorDependency> dependencies)
        {
            Dependencies = dependencies;

            return this;
        }
        public Indicator Update(string name, string description, string formula, List<IndicatorDependency> dependencies)
        {
            Name = name;
            Description = description;
            Formula = formula;
            Dependencies = dependencies;

            return this;
        }
        public Indicator UpdateDependencyTree(string name, string description, string formula)
        {
            Name = name;
            Description = description;
            Formula = formula;

            return this;
        }
    }
}
