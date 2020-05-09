using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Services;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateCurrenciesJob
    {
        private readonly CurrencyService _currencyService;
        private readonly ILogger<UpdateCurrenciesJob> _logger;

        public UpdateCurrenciesJob(
            CurrencyService currencyService,
            ILogger<UpdateCurrenciesJob> logger)
        {
            _currencyService = currencyService;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                await _currencyService.UpdateCurrencies();
            }
            catch (Exception ex)
            {
                // Log into Splunk    
                _logger.LogSplunkInformation(new
                {
                    JobFailed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}