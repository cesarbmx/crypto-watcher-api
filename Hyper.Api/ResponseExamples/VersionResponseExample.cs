using Hyper.Api.FakeResponses;
using Swashbuckle.AspNetCore.Examples;

namespace Hyper.Api.ResponseExamples
{
    public class VersionResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return VersionFakeResponse.GetFake_Production();
        }
    }
}