using Hyper.Api.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

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