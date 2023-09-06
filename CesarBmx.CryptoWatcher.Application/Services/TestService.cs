using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using CesarBmx.CryptoWatcher.Application.Requests;

namespace CesarBmx.CryptoWatcher.Application.Services
{
    public class TestService
    {
        private readonly CoinpaprikaAPI.Client _coinpaprikaClient;
        private readonly ActivitySource _activitySource;
        private readonly ILogger<TestService> _logger;

        public TestService(
            CoinpaprikaAPI.Client coinpaprikaClient,
            ActivitySource activitySource,
            ILogger<TestService> logger)
        {
            _coinpaprikaClient = coinpaprikaClient;
            _activitySource = activitySource;
            _logger = logger;
        }

        public async Task TestLogging(TestLogging request)
        {
            // Start span
            using var span = _activitySource.StartActivity(nameof(TestLogging));

            await _coinpaprikaClient.GetTickersAsync();

            _logger.LogInformation("{@Event}, {@Id}, {@Request}", "LoggingTested", Guid.NewGuid(), request);
        }
    }
}
