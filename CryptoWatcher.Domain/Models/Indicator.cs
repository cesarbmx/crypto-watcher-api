﻿using System;
using System.Collections.Generic;

namespace CryptoWatcher.Domain.Models
{
    public class Indicator
    {
        public string UserId { get; private set; }
        public string IndicatorId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Formula { get; private set; }
        public List<IndicatorDependency> Dependencies { get; private set; }
        public int DependencyLevel { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Indicator() { }
        public Indicator(
            string userId,
            string indicatorId,
            string name, 
            string description,
            string formula,
            List<IndicatorDependency> dependencies,
            int dependencyLevel,
            DateTime createdAt)
        {
            UserId = userId;
            IndicatorId = indicatorId;
            Name = name;
            Description = description;
            Formula = formula;
            Dependencies = dependencies;
            DependencyLevel = dependencyLevel;
            CreatedAt = createdAt;
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
