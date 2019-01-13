using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;

namespace CryptoWatcher.Application.Services
{
    public class LineChartService
    {
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<DataPoint> _lineRepository;
        private readonly IMapper _mapper;

        public LineChartService(
            IRepository<Currency> currencyRepository,
            IRepository<Indicator> indicatorRepository,
            IRepository<DataPoint> lineRepository,
            IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _indicatorRepository = indicatorRepository;
            _lineRepository = lineRepository;
            _mapper = mapper;
        }
        public async Task<List<LineChartResponse>> GetAllLineCharts(string currencyId = null, IndicatorType? indicatorType = null, string indicatorId = null, string userId = null)
        {
            // Get all currencies
            var currencyFilterExxpression = CurrencyExpression.CurrencyFilter(currencyId);
            var currencies = await _currencyRepository.GetAll(currencyFilterExxpression);

            // Get all indicators
            var indicatorFilterExpression = IndicatorExpression.IndicatorFilter(indicatorType, indicatorId, userId);
            var indicators = await _indicatorRepository.GetAll(indicatorFilterExpression);

            // Get all lines
            var lineFilterExpression = LineExpression.LineFilter(currencyId, indicatorType, indicatorId, userId);
            var lines = await _lineRepository.GetAll(lineFilterExpression);

            // Build
            var lineCharts = LineChartBuilder.BuildLineCharts(currencies, indicators, lines);

            // Response
            var response = _mapper.Map<List<LineChartResponse>>(lineCharts);

            // Return
            return response;
        }
    }
}
