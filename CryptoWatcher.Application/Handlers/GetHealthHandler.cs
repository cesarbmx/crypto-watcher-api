using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using MediatR;

namespace CryptoWatcher.Application.Handlers
{
    public class GetHealthHandler : IRequestHandler<GetHealthRequest, HealthResponse>
    {
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IMapper _mapper;

        public GetHealthHandler(IRepository<Currency> currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
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
                // Get all currencies
                await _currencyRepository.GetAll();
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
