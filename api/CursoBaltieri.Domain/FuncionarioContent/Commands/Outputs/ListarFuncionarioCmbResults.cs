using System;

namespace Domain.FuncionarioContent.Commands.Outputs
{
    public class ListarFuncionarioCmbResults
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
    }
}
