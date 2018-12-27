using System.Collections.Generic;
using CryptoWatcher.Application.Logs.Responses;
using MediatR;

namespace CryptoWatcher.Application.Logs.Requests
{
    public class GetAllLogsRequest : IRequest<List<LogResponse>>
    {
        
    }
}
