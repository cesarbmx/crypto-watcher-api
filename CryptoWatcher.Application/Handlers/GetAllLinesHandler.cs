using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using MediatR;

namespace CryptoWatcher.Application.Handlers
{
    public class GetAllLinesHandler : IRequestHandler<GetAllLinesRequest, List<LineResponse>>
    {
        private readonly IRepository<Line> _lineRepository;
        private readonly IMapper _mapper;

        public GetAllLinesHandler(IRepository<Line> lineRepository, IMapper mapper)
        {
            _lineRepository = lineRepository;
            _mapper = mapper;
        }
        public async Task<List<LineResponse>> Handle(GetAllLinesRequest request, CancellationToken cancellationToken)
        {
            // Get all lines
            var lines = await _lineRepository.GetAll();

            // Filter
            lines.FilterLines(request.CurrencyId, request.IndicatorId);

            // Response
            var response = _mapper.Map<List<LineResponse>>(lines);

            // Return
            return response;
        }
    }
}
