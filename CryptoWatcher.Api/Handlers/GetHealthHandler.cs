using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Services;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetHealthHandler : IRequestHandler<GetHealthRequest, HealthResponse>
    {
        private readonly CacheService _cacheService;
        private readonly IMapper _mapper;

        public GetHealthHandler(CacheService cacheService, IMapper mapper)
        {
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<HealthResponse> Handle(GetHealthRequest request, CancellationToken cancellationToken)
        {
            var isEverythingOk = true;
            var isConnectionToDatabaseOk = true;
            var isResponseTimeAcceptable = true;

            // Check if connection to database is ok
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                await _cacheService.GetFromCache<Currency>(CacheKey.Currencies);
                sw.Stop();
            }
            catch
            {
                isEverythingOk = false;
                isConnectionToDatabaseOk = false;
            }

            // Check if response time is ok
            if (sw.ElapsedMilliseconds > 5000)
            {
                isEverythingOk = false;
                isResponseTimeAcceptable = false;
            }

            // Health
            var health = new Health(isEverythingOk, isConnectionToDatabaseOk, isResponseTimeAcceptable);

            // Response
            var response = _mapper.Map<HealthResponse>(health);

            // Return
            return response;
        }
    }
}
