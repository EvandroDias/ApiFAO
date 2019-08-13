using System;

namespace Domain.AlunoContent.Commands.Outputs
{
    public class ListarAlunoResults
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public Boolean Status { get; set; }
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
