

using System;

namespace CryptoWatcher.Domain.Models
{
    public class IndicatorDependency: IEntity
    {
        public string Id => IndicatorId + "_" + DependencyId;
        public string IndicatorId { get; private set; }
        public string DependencyId { get; private set; }
        public DateTime Time { get; private set; }
        public Indicator Dependency { get; private set; }

        public IndicatorDependency() { }
        public IndicatorDependency(string indicatorId, Indicator dependency)
        {
            IndicatorId = indicatorId;
            DependencyId = dependency.IndicatorId;
            Dependency = dependency;
            Time = DateTime.Now;
        }
    }
}
