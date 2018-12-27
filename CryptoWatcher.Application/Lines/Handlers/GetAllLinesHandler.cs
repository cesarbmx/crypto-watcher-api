using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Lines.Requests;
using CryptoWatcher.Application.Lines.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using MediatR;

namespace CryptoWatcher.Application.Lines.Handlers
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
            var lines = await _lineRepository.GetAll(LineExpression.LineFilter(request.CurrencyId, request.IndicatorId));

            // Response
            var response = _mapper.Map<List<LineResponse>>(lines);

            // Return
            return response;
        }
    }
}
