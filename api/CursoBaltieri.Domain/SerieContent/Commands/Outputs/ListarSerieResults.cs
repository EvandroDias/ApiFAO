using System;

namespace Domain.SerieContent.Commands.Outputs
{
    public class ListarSerieResults
    {
        public string Nome { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid Id { get; set; }
        public Boolean Status { get; set; }
    }
}
