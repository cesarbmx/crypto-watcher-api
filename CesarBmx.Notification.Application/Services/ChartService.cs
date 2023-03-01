using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Notification.Domain.Builders;
using CesarBmx.Notification.Domain.Expressions;
using CesarBmx.Notification.Domain.Models;
using CesarBmx.Notification.Domain.Types;
using CesarBmx.Notification.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.Notification.Application.Services
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
        public async Task<List<Responses.Chart>> GetCharts(LineRetention lineRetention, Period period = Period.ONE_MINUTE, List<string> currencyIds = null, List<string> userIds = null, List<string> indicatorIds = null)
        {
            // Get all currencies
            var currencies = await _mainDbContext.Currencies.Where(CurrencyExpression.Filter(currencyIds)).ToListAsync();

            // Get all indicators
            var indicators = await _mainDbContext.Indicators.Where(IndicatorExpression.Filter(indicatorIds)).ToListAsync();

            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.Filter(lineRetention, period, currencyIds, userIds, indicatorIds)).ToListAsync();

            // Build charts
            var charts = ChartBuilder.BuildCharts(currencies, indicators, lines);

            // Response
            var response = _mapper.Map<List<Responses.Chart>>(charts);

            // Return
            return response;
        }
    }
}
