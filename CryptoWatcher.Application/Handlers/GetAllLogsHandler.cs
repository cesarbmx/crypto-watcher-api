using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using MediatR;

namespace CryptoWatcher.Application.Handlers
{
    public class GetAllLogsHandler : IRequestHandler<GetLogsRequest, List<LogResponse>>
    {
        private readonly IRepository<Log> _logRepository;
        private readonly IMapper _mapper;

        public GetAllLogsHandler(IRepository<Log> logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public async Task<List<LogResponse>> Handle(GetLogsRequest request, CancellationToken cancellationToken)
        {
            // Get logs
            var logs = await _logRepository.GetAll();

            // Response
            var response = _mapper.Map<List<LogResponse>>(logs);

            // Return
            return response;
        }
    }
}
