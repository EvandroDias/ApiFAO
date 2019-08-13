using System;

namespace Domain.BimestreContent.Commands.Outputs
{
    public class ListarBimestreResults
    {
        public string Nome { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid Id { get; set; }
        public Boolean Status { get; set; }
    }
}
