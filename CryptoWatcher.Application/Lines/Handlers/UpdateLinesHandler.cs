using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CryptoWatcher.Application.Lines.Requests;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Lines.Handlers
{
    public class UpdateLinesHandler : IRequestHandler<UpdateLinesRequest>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateLinesRequest> _logger;
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<Line> _chartRepository;

        public UpdateLinesHandler(
            MainDbContext mainDbContext,
            ILogger<UpdateLinesRequest> logger,
            IRepository<Currency> currencyRepository,
            IRepository<Indicator> indicatorRepository,
            IRepository<Watcher> watcherRepository,
            IRepository<Line> chartRepository)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _currencyRepository = currencyRepository;
            _indicatorRepository = indicatorRepository;
            _watcherRepository = watcherRepository;
            _chartRepository = chartRepository;
        }

        public async Task<Unit> Handle(UpdateLinesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Get all currencies
                var currencies = await _currencyRepository.GetAll();

                // Get all indicators
                var indicators = await _indicatorRepository.GetAll();

                // Get all watchers with buy or sell
                var watchers = await _watcherRepository.GetAll(WatcherExpression.NonDefaultWatcherWithBuyOrSell());

                // Build charts
                var charts = LineBuilder.BuildLines(currencies, indicators, watchers);

                // Set charts
                _chartRepository.AddRange(charts);

                // Save
                await _mainDbContext.SaveChangesAsync(cancellationToken);

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation(new
                {
                    charts.Count,
                    ExecutionTime = stopwatch.Elapsed.TotalSeconds
                });

                // Return
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Log into Splunk 
                _logger.LogSplunkError(ex);
            }

            // Return
            return Unit.Value;
        }
    }
}
