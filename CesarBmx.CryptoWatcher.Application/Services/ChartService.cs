using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Domain.Types;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.CryptoWatcher.Application.Services
{
    public class ChartService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public ChartService(
            MainDbContext mainDbContext,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
        public async Task<List<Resources.Chart>> GetAllCharts(Period period = Period.ONE_MINUTE, List<string> currencyIds = null, List<string> userIds = null, List<string> indicatorIds = null)
        {
            // Get all currencies
            var currencies = await _mainDbContext.Currencies.Where(CurrencyExpression.Filter(currencyIds)).ToListAsync();

            // Get all indicators
            var indicators = await _mainDbContext.Indicators.Where(IndicatorExpression.Filter(indicatorIds)).ToListAsync();

            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.Filter(period, currencyIds, userIds, indicatorIds)).ToListAsync();

            // Build charts
            var charts = ChartBuilder.BuildCharts(currencies, indicators, lines);

            // Response
            var response = _mapper.Map<List<Resources.Chart>>(charts);

            // Return
            return response;
        }
    }
}
