

namespace CryptoWatcher.Domain.Models
{
    public class Health
    {
        public bool IsEverythingOk { get; set; }
        public bool IsConnectionToDatabaseOk { get; set; }
        public bool IsResponseTimeAcceptable { get; set; }

        public Health(bool isEverythingOk, bool isConnectionToDatabaseOk, bool isResponseTimeAcceptable)
        {
            IsEverythingOk = isEverythingOk;
            IsConnectionToDatabaseOk = isConnectionToDatabaseOk;
            IsResponseTimeAcceptable = isResponseTimeAcceptable;
        }
    }
}
