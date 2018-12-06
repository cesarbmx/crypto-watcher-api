using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Api.Handlers
{
    public class UpdateWatcherHandler : IRequestHandler<UpdateWatcherRequest, WatcherResponse>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly ILogger<UpdateWatcherRequest> _logger;
        private readonly IMapper _mapper;

        public UpdateWatcherHandler(
            MainDbContext mainDbContext,
            IRepository<Watcher> watcherRepository,
            ILogger<UpdateWatcherRequest> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
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
            watcher.Update(request.BuySell, request.Enabled);

            // Save
            await _mainDbContext.SaveChangesAsync(cancellationToken);

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
    }
}
