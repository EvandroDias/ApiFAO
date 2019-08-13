using System;

namespace Domain.AlunoConselhoContent.Commands.Outputs
{
    public class DetalheAlunoConselhoResults
    {
        public Guid AlunoConselhoId { get; set; }
        public Guid ConselhoId { get;  set; }
        public Guid UsuarioId { get;  set; }
        public string Descricao { get;  set; }
        public Guid AlunoId { get;  set; }
        public string NomeAluno { get; set; }
    }
}
