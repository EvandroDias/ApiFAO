using System;
using System.Collections.Generic;

namespace Domain.OcorrenciaContent.Commands.Outputs
{
    public class RetornoTotalOcorrenciaResults
    {
           

        public RetornoTotalOcorrenciaResults(string nomeAluno, List<string> tipoOcorrencia)
        {
            TipoOcorrencia = new List<string>();
            NomeAluno = nomeAluno;
            TipoOcorrencia = tipoOcorrencia;
         
           
        }

        public string NomeAluno { get; set; }
        public List<String> TipoOcorrencia { get; set; }
      
    }
}
