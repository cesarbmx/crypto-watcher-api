

namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {
        public int Percentage { get; private set; }
        public int Trend { get; private set; }

        public Watcher() { }
        public Watcher(string id, int percentage, int trend)
        {
            Id = id;
            Percentage = percentage;
            Trend = trend;
        }
    }
}
