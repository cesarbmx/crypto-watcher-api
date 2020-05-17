using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Persistence.Repositories;
using CryptoWatcher.Domain.ModelBuilders;
using CryptoWatcher.Domain.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class LineService
    {
        private readonly DbContext _dbContext;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<Line> _lineRepository;
        private readonly ILogger<LineService> _logger;
        private readonly IMapper _mapper;

        public LineService(
            DbContext dbContext,
            IRepository<Watcher> watcherRepository,
            IRepository<Line> lineRepository,
            ILogger<LineService> logger, 
            IMapper mapper)
        {
            _dbContext = dbContext;
            _watcherRepository = watcherRepository;
            _lineRepository = lineRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<LineResponse>> GetAllLines(string currencyId = null, IndicatorType? indicatorType = null, string indicatorId = null, string userId = null)
        {
            // Get all lines
            var lines = await _lineRepository.GetAll(LineExpression.LineFilter(currencyId, indicatorType, indicatorId, userId));

            // Response
            var response = _mapper.Map<List<LineResponse>>(lines);

            // Return
            return response;
        }
        public async Task<List<Line>> UpdateLines(List<Currency> currencies, List<Indicator> indicators)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get watchers willing to buy or sell
            var watchers = await _watcherRepository.GetAll(WatcherExpression.WatcherWillingToBuyOrSell());

            // Build new lines
            var lines = LineBuilder.BuildLines(currencies, indicators, watchers);

            // Set new lines
            _lineRepository.AddRange(lines);

            // Save
            await _dbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("UpdateLines",new
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
            var lines = await _lineRepository.GetAll(LineExpression.ObsoleteLine());

            // Remove
            _lineRepository.RemoveRange(lines);

            // Save
            await _dbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("RemoveObsoleteLines", new
            {
                lines.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });
        }
    }
}
