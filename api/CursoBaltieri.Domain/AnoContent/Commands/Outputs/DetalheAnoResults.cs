using System;

namespace Domain.AnoContent.Commands.Outputs
{
    public class DetalheAnoResults
    {
        public string Nome { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid Id { get; set; }
        public Boolean Status { get; set; }
    }
}
