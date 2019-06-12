using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Application.Services
{
    public class LineChartService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public LineChartService(
            MainDbContext mainDbContext,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
        public async Task<List<LineChartResponse>> GetAllLineCharts(string currencyId = null, IndicatorType? indicatorType = null, string indicatorId = null, string userId = null)
        {
            // Get all currencies
            var currencyFilterExxpression = CurrencyExpression.CurrencyFilter(currencyId);
            var currencies = await _mainDbContext.Currencies.Where(currencyFilterExxpression).ToListAsync();

            // Get all indicators
            var indicators = await _mainDbContext.Indicators.Where(IndicatorExpression.IndicatorFilter(userId, indicatorType)).ToListAsync();

            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.LineFilter(currencyId, indicatorType, indicatorId, userId)).ToListAsync();

            // Build
            var lineCharts = LineChartBuilder.BuildLineCharts(currencies, indicators, lines);

            // Response
            var response = _mapper.Map<List<LineChartResponse>>(lineCharts);

            // Return
            return response;
        }
    }
}
