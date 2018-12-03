using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetWatcherHandler : IRequestHandler<GetWatcherRequest, WatcherResponse>
    {
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IMapper _mapper;

        public GetWatcherHandler(
            IRepository<Watcher> watcherRepository,
            IMapper mapper)
        {
            _watcherRepository = watcherRepository;
            _mapper = mapper;
        }

        public async Task<WatcherResponse> Handle(GetWatcherRequest request, CancellationToken cancellationToken)
        {
            // Get watcher
            var watcher = await _watcherRepository.GetSingle(request.WatcherId);

            // Throw NotFound exception if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
    }
}
