using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;

namespace Hyper.Domain.Repositories
{
    public interface ICurrencyRepository
    {
        Task<List<Currency>> GetAllCurrencies();
        Task SetAllCurrencies(List<Currency> currencies);
    }
}