


namespace CryptoWatcher.Domain.Models
{
    public class LineChartColumn
    {
        public string Label { get; private set; }
        public string Type { get; private set; }
       

        public LineChartColumn() { }
        public LineChartColumn(string type, string label)
        {
            Type = type;
            Label = label;
        }
    }
}
