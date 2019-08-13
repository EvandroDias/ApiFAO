using System.Collections.Generic;

namespace Domain.RotinaContent.Commands.Outputs
{
    public class GraficoRotinaResults
    {
        public GraficoRotinaResults()
        {
            Mes = new List<string>();
            LineChartData = new List<LineChartData>();
        }
        public List<LineChartData> LineChartData { get; set; }
        public List<string> Mes { get; set; }
    }
}
