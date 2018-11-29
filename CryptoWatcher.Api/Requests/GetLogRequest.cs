using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetLogRequest : IRequest<LogResponse>
    {
        public string Id { get; set; }
    }
}
