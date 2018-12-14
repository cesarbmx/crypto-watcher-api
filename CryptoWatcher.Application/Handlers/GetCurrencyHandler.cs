using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Application.Handlers
{
    public class GetCurrencyHandler : IRequestHandler<GetCurrencyRequest, CurrencyResponse>
    {
        private readonly CacheService _cacheService;
        private readonly IMapper _mapper;

        public GetCurrencyHandler(CacheService cacheService, IMapper mapper)
        {
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<CurrencyResponse> Handle(GetCurrencyRequest request, CancellationToken cancellationToken)
        {
            // Get currencies
            var currencies = await _cacheService.GetFromCache<Currency>(CacheKey.Currencies);

            // Get currency
            var currency = currencies.FirstOrDefault(x => x.Id == request.CurrencyId);

            // Throw NotFound exception if the currency does not exist
            if (currency == null) throw new NotFoundException(CurrencyMessage.CurrencyNotFound);

            // Response
            var response = _mapper.Map<CurrencyResponse>(currency);

            // Return
            return response;
        }
    }
}
