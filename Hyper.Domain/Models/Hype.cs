

namespace CryptoWatcher.Domain.Models
{
    public class Hype : Entity
    {
        public int Percentage { get; private set; }
        public int Trend { get; private set; }

        public Hype() { }
        public Hype(string id, int percentage, int trend)
        {
            Id = id;
            Percentage = percentage;
            Trend = trend;
        }
    }
}
