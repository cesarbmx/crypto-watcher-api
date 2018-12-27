using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Charts.Requests;
using CryptoWatcher.Application.Charts.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using MediatR;

namespace CryptoWatcher.Application.Charts.Handlers
{
    public class GetAllChartsHandler : IRequestHandler<GetAllChartsRequest, List<ChartResponse>>
    {
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<Line> _lineRepository;
        private readonly IMapper _mapper;

        public GetAllChartsHandler(
            IRepository<Currency> currencyRepository,
            IRepository<Indicator> indicatorRepository,
            IRepository<Line> lineRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _indicatorRepository = indicatorRepository;
            _lineRepository = lineRepository;
            _mapper = mapper;
        }

        public async Task<List<ChartResponse>> Handle(GetAllChartsRequest request, CancellationToken cancellationToken)
        {
            // Get all currencies
            var currencies = await _currencyRepository.GetAll();

            // Get all indicators
            var indicators = await _indicatorRepository.GetAll();

            // Get all lines
            var lines = await _lineRepository.GetAll();

            // Build charts
            var charts = ChartBuilder.BuildCharts(currencies, indicators, lines);

            // Response
            var response = _mapper.Map<List<ChartResponse>>(charts);

            // Return
            return response;
        }
    }
}
