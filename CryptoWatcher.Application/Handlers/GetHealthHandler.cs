using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoinMarketCap.Core;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using MediatR;

namespace CryptoWatcher.Application.Handlers
{
    public class GetHealthHandler : IRequestHandler<GetHealthRequest, HealthResponse>
    {
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IMapper _mapper;
        private readonly ICoinMarketCapClient _coinMarketCapClient;

        public GetHealthHandler(
            IRepository<Currency> currencyRepository,
            IMapper mapper,
            ICoinMarketCapClient coinMarketCapClient)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
            _coinMarketCapClient = coinMarketCapClient;
        }

        public async Task<HealthResponse> Handle(GetHealthRequest request, CancellationToken cancellationToken)
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
