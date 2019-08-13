using System;

namespace Domain.DisciplinaContent.Commands.Outputs
{
    public class ListarDisciplinaResults
    {
        public string Nome { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid Id { get; set; }
        public Boolean Status { get; set; }
    }
}
