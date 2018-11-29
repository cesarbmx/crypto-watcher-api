using System.Collections.Generic;
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
    public class GetCurrenciesHandler : IRequestHandler<GetCurrenciesRequest, List<CurrencyResponse>>
    {
        private readonly CacheService _cacheService;
        private readonly IMapper _mapper;

        public GetCurrenciesHandler(CacheService cacheService, IMapper mapper)
        {
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<List<CurrencyResponse>> Handle(GetCurrenciesRequest request, CancellationToken cancellationToken)
        {
            // Get currencies
            var currencies = await _cacheService.GetFromCache<Currency>();

            // Response
            var response = _mapper.Map<List<CurrencyResponse>>(currencies);

            // Return
            return response;
        }
    }
}
