using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetLogRequest : IRequest<LogResponse>
    {
        public string LogId { get; set; }
    }
}
