using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Application.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class VersionResponseExample : IExamplesProvider<VersionResponse>
    {
        public VersionResponse GetExamples()
        {
            return VersionFakeResponse.GetFake_Production();
        }
    }
}