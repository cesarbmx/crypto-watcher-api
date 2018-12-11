using System.Collections.Generic;
using System.Linq;
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
    public class GetAllLinesHandler : IRequestHandler<GetAllLinesRequest, List<LineResponse>>
    {
        private readonly CacheService _cacheService;
        private readonly IMapper _mapper;

        public GetAllLinesHandler(
            CacheService cacheService,
            IMapper mapper)
        {
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<List<LineResponse>> Handle(GetAllLinesRequest request, CancellationToken cancellationToken)
        {
            // Get all lines
            var lines = await _cacheService.GetFromCache<Line>(CacheKey.Lines);

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
