using CryptoWatcher.Shared.Responses;

namespace CryptoWatcher.Shared.FakeResponses
{
    public static class VersionFakeResponse
    {
        public static VersionResponse GetFake_Production()
        {
            return new VersionResponse
            {
                VersionNumber = "1.0.0.0",
                BuildDateTime = "2017/08/30 06:20",
                LastBuildOccurred = "40 seconds ago",
                Environment = "Production"
            };
        }
    }
}
