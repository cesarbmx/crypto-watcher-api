using System;
using System.Threading.Tasks;
using Hangfire;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Services;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateOrdersJob
    {
        private readonly OrderService _orderService;
        private readonly ILogger<UpdateOrdersJob> _logger;

        public UpdateOrdersJob(
            OrderService orderService,
            ILogger<UpdateOrdersJob> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                await _orderService.UpdateOrders();
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