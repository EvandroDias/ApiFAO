using System;

namespace Domain.EscolaContent.Commands.Outputs
{
    public class ListarEscolaResults
    {
        public string Nome { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid Id { get; set; }
    }
}
