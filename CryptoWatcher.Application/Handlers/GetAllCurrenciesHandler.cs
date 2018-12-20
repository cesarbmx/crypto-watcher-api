using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Repositories;
using MediatR;

namespace CryptoWatcher.Application.Handlers
{
    public class GetAllCurrenciesHandler : IRequestHandler<GetAllCurrenciesRequest, List<CurrencyResponse>>
    {
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IMapper _mapper;

        public GetAllCurrenciesHandler(IRepository<Currency> currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        public async Task<List<CurrencyResponse>> Handle(GetAllCurrenciesRequest request, CancellationToken cancellationToken)
        {
            // Get all currencies
            var currencies = await _currencyRepository.GetAll();

            // Response
            var response = _mapper.Map<List<CurrencyResponse>>(currencies);

            // Return
            return response;
        }
    }
}
