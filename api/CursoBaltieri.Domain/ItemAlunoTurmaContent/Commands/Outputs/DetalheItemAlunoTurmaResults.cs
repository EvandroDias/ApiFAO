using System;

namespace Domain.AlunoTurmaContent.Commands.Inputs
{
    public class DetalheAlunoTurmaResults
    {
        public Guid AlunoId { get; set; }
        public Guid TurmaId { get; set; }
        public int Numero { get; set; }
        public string Status { get; set; }

    }
}
