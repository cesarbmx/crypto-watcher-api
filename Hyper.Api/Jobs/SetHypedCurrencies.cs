using System;
using System.Threading.Tasks;
using Hangfire;
using Hyper.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Hyper.Api.Jobs
{
    public class SetHypedCurrencies
    {
        readonly ILogger<SetHypedCurrencies> _logger;


        public SetHypedCurrencies(
            ILogger<SetHypedCurrencies> logger)
        {
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Execute()
        {
            try
            {
                // Log into Splunk
                _logger.LogInformation(LoggingEvents.HypedCurrenciesHaveBeenSet, "Hyped currencies have been set");

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogError(LoggingEvents.SettingHypedCurrenciesHasFailed, ex, "Setting hyped currencies has failed");
            }
        }
    }
}