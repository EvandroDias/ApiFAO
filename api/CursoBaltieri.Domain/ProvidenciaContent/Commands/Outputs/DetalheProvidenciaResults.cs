using System;

namespace Domain.ProvidenciaContent.Commands.Outputs
{
    public class DetalheProvidenciaResults
    {
        public string Nome { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid Id { get; set; }
        public Boolean Status { get; set; }
    }
}
