using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.Extensions.Logging;
using Hyper.Domain.Repositories;
using NoobsMuc.Coinmarketcap.Client;
using Hyper.Infrastructure.Contexts;
using System.Linq;

namespace Hyper.Infrastructure.Jobs
{
    public class CurrencyJob
    {
        private readonly IMapper _mapper;
        readonly ILogger<CurrencyJob> _logger;
        private readonly MainDbContext _mainDbContext;
        private readonly ICoinmarketcapClient _coinmarketcapClient;
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyJob(IMapper mapper, ILogger<CurrencyJob> logger, MainDbContext mainDbContext, ICoinmarketcapClient coinmarketcapClient, ICurrencyRepository currencyRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _mainDbContext = mainDbContext;
            _coinmarketcapClient = coinmarketcapClient;
            _currencyRepository = currencyRepository;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Import()
        {
            try
            {
                // Get all currencies from CoinMarketCap
                var result = _coinmarketcapClient.GetCurrencies(1);

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