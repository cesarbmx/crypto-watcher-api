using System.Collections.Generic;
using Hyper.Domain.Models;

namespace Hyper.Domain.Events
{
    public class AllCurrenciesImported : IEvent
    {
        public List<Currency> Currencies { get; set; }

        public AllCurrenciesImported(List<Currency> currencies)
        {
            Currencies = currencies;
        }
    }
}
