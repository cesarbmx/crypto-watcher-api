using System.Collections.Generic;
using Hyper.Domain.Models;

namespace Hyper.Domain.Events
{
    public class AllCurrenciesImported : IEvent
    {
        public IEnumerable<Currency> Currencies { get; set; }

        public AllCurrenciesImported(IEnumerable<Currency> currencies)
        {
            Currencies = currencies;
        }
    }
}
