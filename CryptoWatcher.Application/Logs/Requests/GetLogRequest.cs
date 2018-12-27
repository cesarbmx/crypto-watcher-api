using System;
using CryptoWatcher.Application.Logs.Responses;
using MediatR;

namespace CryptoWatcher.Application.Logs.Requests
{
    public class GetLogRequest : IRequest<LogResponse>
    {
        public Guid LogId { get; set; }
    }
}
