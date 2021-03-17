


using System.Collections.Generic;

namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class Indicator
    {
        public string UserId { get; set; }
        public string IndicatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Formula { get; set; }
        public List<string> Dependencies { get; set; }
    }
}
