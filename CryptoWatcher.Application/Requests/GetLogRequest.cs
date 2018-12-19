using System;
using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetLogRequest : IRequest<LogResponse>
    {
        public Guid LogId { get; set; }
    }
}
