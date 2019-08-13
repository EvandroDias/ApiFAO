using System;

namespace Domain.SerieContent.Commands.Outputs
{
    public class DetalheSerieResults
    {
        public string Nome { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid Id { get; set; }
        public Boolean Status { get; set; }
    }
}
