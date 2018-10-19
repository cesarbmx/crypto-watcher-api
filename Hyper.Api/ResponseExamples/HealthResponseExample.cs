using Hyper.Api.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace Hyper.Api.ResponseExamples
{
    public class HealthResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new HealthResponse { IsEverythingOk = true, IsConnectionToDatabaseOk = true, IsResponseTimeAcceptable = true};
        }
    }
}