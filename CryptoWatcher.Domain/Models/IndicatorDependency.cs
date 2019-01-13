

namespace CryptoWatcher.Domain.Models
{
    public class IndicatorDependency: IEntity
    {
        public string Id => IndicatorId + "_" + DependsOn;
        public string IndicatorId { get; private set; }
        public string DependsOn { get; private set; }
        public int Level { get; private set; }

        public IndicatorDependency() { }
        public IndicatorDependency(string indicatorId, string dependsOn, int level)
        {
            IndicatorId = indicatorId;
            DependsOn = dependsOn;
            Level = level;
        }

        public IndicatorDependency SetLevel(int level)
        {
            Level = level;

            return this;
        }
    }
}
