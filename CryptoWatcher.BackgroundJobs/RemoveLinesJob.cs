using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Contexts;
using CryptoWatcher.Shared.Extensions;
using CryptoWatcher.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class RemoveLinesJob
    {
        private readonly IContext _context;
        private readonly ILogger<RemoveLinesJob> _logger;
        private readonly IRepository<Line> _lineRepository;

        public RemoveLinesJob(
            IContext context,
            ILogger<RemoveLinesJob> logger,
            IRepository<Line> lineRepository)
        {
            _context = context;
            _logger = logger;
            _lineRepository = lineRepository;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Get all lines
                var lines = await _lineRepository.GetAll(LineExpression.ObsoleteLine());

                // Remove
                _lineRepository.RemoveRange(lines);

                // Save
                await _context.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation(new
                {
                    lines.Count,
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
        }
    }
}