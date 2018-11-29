using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Api.Handlers
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
            var currencies = await _cacheService.GetFromCache<Currency>();

            // Get currency
            var currency = currencies.FirstOrDefault(x => x.Id == request.Id);

            // Throw NotFound exception if the currency does not exist
            if (currency == null) throw new NotFoundException(CurrencyMessage.CurrencyNotFound);

            // Response
            var response = _mapper.Map<CurrencyResponse>(currency);

            // Return
            return response;
        }
    }
}
