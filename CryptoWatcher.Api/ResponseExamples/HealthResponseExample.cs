using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class HealthResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new HealthResponse { IsEverythingOk = true, IsConnectionToDatabaseOk = true, IsResponseTimeAcceptable = true};
        }
    }
}