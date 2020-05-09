using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CoinMarketCap.Core;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Persistence.Repositories;

namespace CryptoWatcher.Application.Services
{
    public class StatusService
    {
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IMapper _mapper;
        private readonly ICoinMarketCapClient _coinMarketCapClient;

        public StatusService(
            IRepository<Currency> currencyRepository,
            IMapper mapper,
            ICoinMarketCapClient coinMarketCapClient)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
            _coinMarketCapClient = coinMarketCapClient;
        }

        public Task<VersionResponse> GetVersion()
        {
            // Get version
            var version = VersionBuilder.BuildVersion(Assembly.GetExecutingAssembly());

            // Response
            var response = _mapper.Map<VersionResponse>(version);

            // Return
            return Task.FromResult(response);
        }
        public async Task<HealthResponse> GetHealth()
        {
            var isEverythingOk = true;
            var isConnectionToDatabaseOk = true;
            var isConnectionToCoinMarketCapOk = true;
            var isResponseTimeAcceptable = true;

            // Start
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Check if connection to database is ok
            try
            {
                await _currencyRepository.GetAll();
                stopwatch.Stop();
            }
            catch
            {
                isEverythingOk = false;
                isConnectionToDatabaseOk = false;
            }

            // Check if connection to CoinMarketCap is ok
            try
            {
                await _coinMarketCapClient.GetTickerListAsync(1);
            }
            catch
            {
                isEverythingOk = false;
                isConnectionToCoinMarketCapOk = false;
            }

            // Check if response time is ok
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds > 5000)
            {
                isEverythingOk = false;
                isResponseTimeAcceptable = false;
            }

            // Health
            var health = new Health(isEverythingOk, isConnectionToDatabaseOk, isConnectionToCoinMarketCapOk, isResponseTimeAcceptable);

            // Response
            var response = _mapper.Map<HealthResponse>(health);

            // Return
            return response;
        }
    }
}
