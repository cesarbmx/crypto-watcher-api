using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CryptoWatcher.Application.Lines.Requests;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Lines.Handlers
{
    public class RemoveOldLinesHandler : IRequestHandler<RemoveOldLinesRequest>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<RemoveOldLinesRequest> _logger;
        private readonly IRepository<Line> _lineRepository;

        public RemoveOldLinesHandler(
            MainDbContext mainDbContext,
            ILogger<RemoveOldLinesRequest> logger,
            IRepository<Line> lineRepository)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _lineRepository = lineRepository;
        }

        public async Task<Unit> Handle(RemoveOldLinesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Get old lines
                var lines = await _lineRepository.GetAll(LineExpression.OldLine());

                // Remove
                _lineRepository.RemoveRange(lines);

                // Save
                await _mainDbContext.SaveChangesAsync(cancellationToken);

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation(new
                {
                    lines.Count,
                    ExecutionTime = stopwatch.Elapsed.TotalSeconds
                });
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
