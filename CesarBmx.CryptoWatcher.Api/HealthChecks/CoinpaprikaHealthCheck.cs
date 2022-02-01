using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CesarBmx.CryptoWatcher.Api.HealthChecks
{
    public class CoinpaprikaHealthCheck : IHealthCheck
    {
        private readonly CoinpaprikaAPI.Client _coinpaprikaClient;


        public CoinpaprikaHealthCheck(CoinpaprikaAPI.Client coinpaprikaClient)
        {
            _coinpaprikaClient = coinpaprikaClient;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {

            var response = await _coinpaprikaClient.GetClobalsAsync();

            if (response.Value != null) return HealthCheckResult.Healthy("https://api.coinpaprika.com/");

            // Return result
            return HealthCheckResult.Unhealthy("Coinpaprika API");
        }
    }
}