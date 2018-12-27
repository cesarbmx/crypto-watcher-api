using CryptoWatcher.Application.System.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class VersionResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return VersionFakeResponse.GetFake_Production();
        }
    }
}