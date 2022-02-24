﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.CryptoWatcher.Application.Queries;
using CesarBmx.CryptoWatcher.Application.Settings;
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
        private readonly AppSettings _appSettings;

        public LineService(
            MainDbContext mainDbContext,
            ILogger<LineService> logger, 
            IMapper mapper,
            AppSettings appSettings)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _mapper = mapper;
            _appSettings = appSettings;
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
        public async Task<List<Line>> AddNewLines(List<Currency> currencies, List<Indicator> indicators)
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

            // Log
            _logger.LogInformation("{@Event}, {@Count}, {@ExecutionTime}", "NewLinesAdded", lines.Count, stopwatch.Elapsed.TotalSeconds);

            // Return
            return lines;
        }
        public async Task RemoveObsoleteLines()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get lines to be removed
            var lines = await _mainDbContext.Lines.Where(LineExpression.ObsoleteLine(_appSettings.LineRetention)).ToListAsync();

            // Remove
            _mainDbContext.Lines.RemoveRange(lines);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log
            _logger.LogInformation("{@Event}, {@Count}, {@ExecutionTime}", "ObsoleteLinesRemoved", lines.Count, stopwatch.Elapsed.TotalSeconds);
        }
    }
}
