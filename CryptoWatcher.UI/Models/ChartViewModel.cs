using System.Collections.Generic;


namespace CryptoWatcher.UI.Models
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