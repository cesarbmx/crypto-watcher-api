using System.Collections.Generic;


namespace CryptoWatcher.Web.Models
{
    public class ChartViewModel
    {
        public List<Chart> Charts { get; set; }

        public ChartViewModel()
        {
            Charts = new List<Chart>();
        }
    }
}