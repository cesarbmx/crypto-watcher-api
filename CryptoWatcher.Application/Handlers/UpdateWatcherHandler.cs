using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Contexts;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using CryptoWatcher.Shared.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Handlers
{
    public class UpdateWatcherHandler : IRequestHandler<UpdateWatcherRequest, WatcherResponse>
    {
        private readonly IContext _context;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly ILogger<UpdateWatcherRequest> _logger;
        private readonly IMapper _mapper;

        public UpdateWatcherHandler(
            IContext context,
            IRepository<Watcher> watcherRepository,
            ILogger<UpdateWatcherRequest> logger,
            IMapper mapper)
        {
            _context = context;
            _watcherRepository = watcherRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<WatcherResponse> Handle(UpdateWatcherRequest request, CancellationToken cancellationToken)
        {
            // Get watcher
            var watcher = await _watcherRepository.GetSingle(request.WatcherId);

            // Throw NotFound exception if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Update
            watcher.Update(request.Buy, request.Sell, request.Enabled);
            _watcherRepository.Update(watcher);

            // Save
            await _context.SaveChangesAsync(cancellationToken);

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
    }
}
