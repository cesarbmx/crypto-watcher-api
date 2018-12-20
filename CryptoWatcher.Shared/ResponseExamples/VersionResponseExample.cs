using CryptoWatcher.Shared.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Shared.ResponseExamples
{
    public class VersionResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return VersionFakeResponse.GetFake_Production();
        }
    }
}