

namespace CryptoWatcher.Api.Responses
{
    public class HealthResponse
    {
        public bool IsEverythingOk { get; set; }
        public bool IsConnectionToDatabaseOk { get; set; }
        public bool IsResponseTimeAcceptable { get; set; }
    }
}
