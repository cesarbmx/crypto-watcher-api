using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Domain.Types;


namespace CesarBmx.CryptoWatcher.Application.Queries
{
    public class GetLines
    {
        public Period Period { get; set; }
        public List<string> CurrencyIds { get; set; }
        public List<string> UserIds { get; set; }
        public List<string> IndicatorIds { get; set; }
    }
}
