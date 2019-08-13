using System.Collections.Generic;

namespace Domain.OcorrenciaContent.Commands.Outputs
{
    public class GraficoOcorrenciaResults2
    {
        public GraficoOcorrenciaResults2()
        {
            
            LineChartData = new List<LineChartData>();
        }
        public List<LineChartData> LineChartData { get; set; }
        public string[] nomes  { get; set; }
    }
}
