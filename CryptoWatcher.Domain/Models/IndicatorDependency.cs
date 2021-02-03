using System;
using CesarBmx.Shared.Domain.Models;


namespace CryptoWatcher.Domain.Models
{
    public class IndicatorDependency: IEntity<IndicatorDependency>
    {
        public string Id => UserId + "_" + IndicatorId + "_" + DependencyUserId + "_" + DependencyIndicatorId;

        public string UserId { get; private set; }
        public string IndicatorId { get; private set; }
        public string DependencyUserId { get; private set; }
        public string DependencyIndicatorId { get; private set; }
        public DateTime Time { get; private set; }

        public IndicatorDependency() { }
        public IndicatorDependency(string userId, string indicatorId, string dependencyUserId, string dependencyIndicatorId, DateTime time)
        {
            UserId = userId;
            IndicatorId = indicatorId;
            DependencyUserId = dependencyUserId;
            DependencyIndicatorId = dependencyIndicatorId;
            Time = time;
        }

        public IndicatorDependency Update(IndicatorDependency indicatorDependency)
        {
            DependencyIndicatorId = indicatorDependency.DependencyIndicatorId;
            Time = indicatorDependency.Time;

            return this;
        }
    }
}
