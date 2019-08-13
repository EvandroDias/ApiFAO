using System.Collections.Generic;

namespace Domain.RotinaContent.Commands.Outputs
{
    public class LineChartData
    {
        public LineChartData()
        {
            Data = new List<int>();
        }
        public List<int> Data { get; set; }
        public string Label { get; set; }
    }
}
