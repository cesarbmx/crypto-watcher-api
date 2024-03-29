﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.CryptoWatcher.Application.Settings;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Types;
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
        private readonly ActivitySource _activitySource;

        public LineService(
            MainDbContext mainDbContext,
            ILogger<LineService> logger, 
            IMapper mapper,
            AppSettings appSettings,
            ActivitySource activitySource)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _mapper = mapper;
            _appSettings = appSettings;
            _activitySource = activitySource;
        }

        public async Task<List<Responses.Line>> GetLines(Period period, List<string> currencyIds, List<string> userIds, List<string> indicatorIds)
        {
            // Start span
            using var span = _activitySource.StartActivity(nameof(GetLines));

            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.Filter(_appSettings.LineRetention, period, currencyIds, userIds, indicatorIds)).ToListAsync();

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

            // Start span
            using var span = _activitySource.StartActivity(nameof(CreateNewLines));

            // Get watchers willing to buy or sell
            var watchers = await _mainDbContext.Watchers.Where(x=>x.Status == WatcherStatus.SET).ToListAsync();

            // Build new lines
            var lines = LineBuilder.BuildLines(currencies, indicators, watchers);

            // Set new lines
            _mainDbContext.Lines.AddRange(lines);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log
            _logger.LogInformation("{@Event}, {@Id}, {@Count}, {@ExecutionTime}", nameof(CreateNewLines), Guid.NewGuid(), lines.Count, stopwatch.Elapsed.TotalSeconds);

            // Return
            return lines;
        }
        public async Task DeleteObsoleteLines()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Start span
            using var span = _activitySource.StartActivity(nameof(DeleteObsoleteLines));

            // Get lines to be removed
            var lines = await _mainDbContext.Lines.Where(LineExpression.ObsoleteLine(_appSettings.LineRetention)).ToListAsync();

            // Remove
            _mainDbContext.Lines.RemoveRange(lines);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log
            _logger.LogInformation("{@Event}, {@Id}, {@Count}, {@ExecutionTime}", nameof(DeleteObsoleteLines), Guid.NewGuid(), lines.Count, stopwatch.Elapsed.TotalSeconds);
        }
    }
}
