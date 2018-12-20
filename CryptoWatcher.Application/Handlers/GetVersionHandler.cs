using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Shared.Requests;
using CryptoWatcher.Shared.Responses;
using MediatR;

namespace CryptoWatcher.Application.Handlers
{
    public class GetVersionHandler : IRequestHandler<GetVersionRequest, VersionResponse>
    {
        private readonly IMapper _mapper;

        public GetVersionHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<VersionResponse> Handle(GetVersionRequest request, CancellationToken cancellationToken)
        {
            // Get version
            var version = VersionBuilder.BuildVersion(Assembly.GetExecutingAssembly());

            // Response
            var response = _mapper.Map<VersionResponse>(version);

            // Return
            return Task.FromResult(response);
        }
    }
}
