using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.CryptoWatcher.Application.Queries;
using CesarBmx.Shared.Logging.Extensions;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CesarBmx.CryptoWatcher.Application.Services
{
    public class LineService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<LineService> _logger;
        private readonly IMapper _mapper;

        public LineService(
            MainDbContext mainDbContext,
            ILogger<LineService> logger, 
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Responses.Line>> GetLines(GetLines query)
        {
            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.Filter(query.Period, query.CurrencyIds, query.UserIds, query.IndicatorIds)).ToListAsync();

            // Response
            var response = _mapper.Map<List<Responses.Line>>(lines);

            // Return
            return response;
        }
        public async Task<List<Line>> CreateNewLines(List<Currency> currencies, List<Indicator> indicators)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get watchers willing to buy or sell
            var watchers = await _mainDbContext.Watchers.Where(WatcherExpression.WatcherBuyingOrSelling()).ToListAsync();

            // Build new lines
            var lines = LineBuilder.BuildLines(currencies, indicators, watchers);

            // Set new lines
            _mainDbContext.Lines.AddRange(lines);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation(nameof(CreateNewLines), new
            {
                lines.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return
            return lines;
        }
        public async Task RemoveObsoleteLines()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get lines to be removed
            var lines = await _mainDbContext.Lines.Where(LineExpression.ObsoleteLine()).ToListAsync();

            // Remove
            _mainDbContext.Lines.RemoveRange(lines);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation(nameof(RemoveObsoleteLines), new
            {
                lines.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });
        }
    }
}
