

namespace CryptoWatcher.Shared.Models
{
    public class Health
    {
        public bool IsEverythingOk { get; private set; }
        public bool IsConnectionToDatabaseOk { get; private set; }
        public bool IsConnectionToCoinMarketCapOk { get; private set; }
        public bool IsResponseTimeAcceptable { get; private set; }

        public Health(bool isEverythingOk, bool isConnectionToDatabaseOk, bool isConnectionToCoinMarketCapOk, bool isResponseTimeAcceptable)
        {
            IsEverythingOk = isEverythingOk;
            IsConnectionToDatabaseOk = isConnectionToDatabaseOk;
            IsConnectionToCoinMarketCapOk = isConnectionToCoinMarketCapOk;
            IsResponseTimeAcceptable = isResponseTimeAcceptable;
        }
    }
}
