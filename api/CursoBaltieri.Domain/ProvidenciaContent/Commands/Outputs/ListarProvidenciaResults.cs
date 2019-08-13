using System;

namespace Domain.ProvidenciaContent.Commands.Outputs
{
    public class ListarProvidenciaResults
    {
        public string Nome { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid Id { get; set; }
        public Boolean Status { get; set; }
    }
}
