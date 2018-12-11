using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetAllLinesHandler : IRequestHandler<GetAllLinesRequest, List<LineResponse>>
    {
        private readonly CacheService _cacheService;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IMapper _mapper;

        public GetAllLinesHandler(
            CacheService cacheService,
            IRepository<Indicator> indicatorRepository,
            IMapper mapper)
        {
            _cacheService = cacheService;
            _indicatorRepository = indicatorRepository;
            _mapper = mapper;
        }
        public async Task<List<LineResponse>> Handle(GetAllLinesRequest request, CancellationToken cancellationToken)
        {
            // Get all currencies
            var currencies = await _cacheService.GetFromCache<Currency>(CacheKey.Currencies);

            // Get currency
            var currency = currencies.FirstOrDefault(x => x.Id == request.CurrencyId);

            // Throw NotFound exception if the currency does not exist
            if (currency == null) throw new NotFoundException(CurrencyMessage.CurrencyNotFound);

            // Get indicator
            var indicator = await _indicatorRepository.GetSingle(request.IndicatorId);

            // Throw NotFound exception if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Get all lines
            var lines = await _cacheService.GetFromCache<Line>(CacheKey.Currencies);

            // Filter
            if (!string.IsNullOrEmpty(request.CurrencyId))
                lines = lines.Where(x => x.CurrencyId == request.CurrencyId).ToList();
            if (!string.IsNullOrEmpty(request.IndicatorId))
                lines = lines.Where(x => x.IndicatorId == request.IndicatorId).ToList();

            // Response
            var response = _mapper.Map<List<LineResponse>>(lines);

            // Return
            return response;
        }
    }
}
