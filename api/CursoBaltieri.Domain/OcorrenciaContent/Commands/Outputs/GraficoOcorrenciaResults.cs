using System.Collections.Generic;

namespace Domain.OcorrenciaContent.Commands.Outputs
{
    public class GraficoOcorrenciaResults
    {
        public GraficoOcorrenciaResults()
        {
            Mes = new List<string>();
            LineChartData = new List<LineChartData>();
        }
        public List<LineChartData> LineChartData { get; set; }
        public List<string> Mes { get; set; }
    }
}
