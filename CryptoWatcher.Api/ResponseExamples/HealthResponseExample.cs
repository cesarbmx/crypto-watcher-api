using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class HealthResponseExample : IExamplesProvider<HealthResponse>
    {
        public HealthResponse GetExamples()
        {
            return new HealthResponse
            {
                IsEverythingOk = true,
                IsConnectionToDatabaseOk = true,
                IsConnectionToCoinMarketCapOk = true,
                IsResponseTimeAcceptable = true
            };
        }
    }
}