

namespace CryptoWatcher.Application.Responses
{
    public class VersionResponse
    {
        public string VersionNumber { get; set; }
        public string BuildDateTime { get; set; }
        public string LastBuildOccurred { get; set; }
        public string Environment { get; set; }
    }
}
