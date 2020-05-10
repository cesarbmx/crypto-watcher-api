using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Persistence.Repositories;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Types;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class LineService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<IndicatorDependency> _indicatorDependencyRepository;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<Line> _lineRepository;
        private readonly ILogger<LineService> _logger;
        private readonly IMapper _mapper;

        public LineService(
            MainDbContext mainDbContext,
            IRepository<Currency> currencyRepository,
            IRepository<Indicator> indicatorRepository,
            IRepository<IndicatorDependency> indicatorDependencyRepository,
            IRepository<Watcher> watcherRepository,
            IRepository<Line> lineRepository,
            ILogger<LineService> logger, 
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _currencyRepository = currencyRepository;
            _indicatorRepository = indicatorRepository;
            _indicatorDependencyRepository = indicatorDependencyRepository;
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
        public async Task UpdateLines()
        {
            // Start watch

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Time
            var time = DateTime.Now;

            // Get all currencies
            var currencies = await _currencyRepository.GetAll();

            // Get all indicators
            var indicators = await _indicatorRepository.GetAll();

            // Get all indicator dependencies
            var indicatorDependencies = await _indicatorDependencyRepository.GetAll();

            // Build indicator dependencies
            IndicatorBuilder.BuildDependencies(indicators, indicatorDependencies);

            // Get non-default watchers with buy or sell
            var watchers = await _watcherRepository.GetAll(WatcherExpression.WatcherWillingToBuyOrSell());

            // Build new lines
            var lines = LineBuilder.BuildLines(currencies, indicators, watchers, time);

            // Set new lines
            _lineRepository.AddRange(lines, time);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("UpdateLines",new
            {
                lines.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });
        }
        public async Task RemoveObsoleteLines()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Time
            var time = DateTime.Now;

            // Get lines to be removed
            var lines = await _lineRepository.GetAll(LineExpression.ObsoleteLine());

            // Remove
            _lineRepository.RemoveRange(lines, time);

            // Save
            await _mainDbContext.SaveChangesAsync();

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
