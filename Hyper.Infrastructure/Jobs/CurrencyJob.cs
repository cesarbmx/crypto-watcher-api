using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoinMarketCap.Core;
using Hangfire;
using Microsoft.Extensions.Logging;
using Hyper.Domain.Repositories;
using Hyper.Infrastructure.Contexts;

namespace Hyper.Infrastructure.Jobs
{
    public class CurrencyJob
    {
        private readonly IMapper _mapper;
        readonly ILogger<CurrencyJob> _logger;
        private readonly MainDbContext _mainDbContext;
        private readonly ICoinMarketCapClient _coinMarketCapClient;
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyJob(IMapper mapper, ILogger<CurrencyJob> logger, MainDbContext mainDbContext, ICoinMarketCapClient coinMarketCapClient, ICurrencyRepository currencyRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _mainDbContext = mainDbContext;
            _coinMarketCapClient = coinMarketCapClient;
            _currencyRepository = currencyRepository;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Import()
        {
            try
            {
                // Get all currencies from CoinMarketCap
                var result = await _coinMarketCapClient.GetTickerListAsync(5);

                // Map to our Model
                var currencies = _mapper.Map<IEnumerable<Domain.Models.Currency>>(result);

                // Set all currencies
                await _currencyRepository.SetAllCurrencies(currencies);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Log into Splunk
                _logger.LogInformation("Event=ImportCountriesCompleted");
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogError(ex, "Event=ImportCountriesFailed");
            }
        }
    }
}