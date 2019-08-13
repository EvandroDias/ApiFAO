using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.OcorrenciaContent.Commands.Outputs
{
    public class FiltroOcorrenciaResults
    {
        public List<ListarOcorrenciaResults> ListaOcorrencia { get; set; }
        public int Quantidade { get; set; }
    }
}
