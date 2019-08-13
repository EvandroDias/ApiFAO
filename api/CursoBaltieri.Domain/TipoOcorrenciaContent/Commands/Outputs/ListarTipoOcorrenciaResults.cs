using System;

namespace Domain.TipoOcorrenciaContent.Commands.Outputs
{
    public class ListarTipoOcorrenciaResults
    {
        public string Nome { get; set; }
        public Guid Id { get; set; }
        public Boolean Status { get; set; }
    }
}
