using System;

namespace Domain.AlunoContent.Commands.Outputs
{
    public class ListarAlunoCmbResults
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public int Numero { get; set; }
        public string Status { get; set; }
        public Guid Id { get; set; }
    }
}
