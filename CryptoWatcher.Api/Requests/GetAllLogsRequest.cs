using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetAllLogsRequest : IRequest<List<LogResponse>>
    {
        
    }
}
