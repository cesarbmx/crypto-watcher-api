using System.Collections.Generic;


namespace CryptoWatcher.UI.Models
{
    public class LineChartViewModel
    {
        public List<LineChart> LineCharts { get; set; }

        public LineChartViewModel()
        {
            LineCharts = new List<LineChart>();
        }
    }
}