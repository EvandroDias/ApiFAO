using System;

namespace Domain.ConselhoContent.Commands.Outputs
{
    public class ListarAlunoConselhoResults
    {
        public Guid AlunoConselhoId { get; set; }
        public Guid Id { get; set; }
        public string NomeAluno { get; set; }

        public string Descricao { get; set; }

        public Guid AlunoId { get; set; }

        public Guid ConselhoId { get; set; }
        public Guid UsuarioId { get; set; }


    }
}
