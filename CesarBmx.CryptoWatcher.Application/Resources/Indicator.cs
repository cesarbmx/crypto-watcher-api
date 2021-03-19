using System.Collections.Generic;


namespace CesarBmx.CryptoWatcher.Application.Resources
{
    public class Indicator
    {
        public string IndicatorId { get; set; }
        public string UserId { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Formula { get; set; }
        public List<IndicatorDependency> Dependencies { get; set; }
    }
}
