using System.Collections.Generic;

namespace Domain.OcorrenciaContent.Commands.Outputs
{
    public class PainelGraficoOcorrenciaResults
    {
        public PainelGraficoOcorrenciaResults()
        {
            MesSerie = new List<string>();
            LineChartDataSerie = new List<LineChartData>();

             MesAluno = new List<string>();
            LineChartDataAluno = new List<LineChartData>();
        }
        public List<LineChartData> LineChartDataSerie { get; set; }
        public List<string> MesSerie { get; set; }

        public List<LineChartData> LineChartDataAluno { get; set; }
        public List<string> MesAluno { get; set; }
    }
}
