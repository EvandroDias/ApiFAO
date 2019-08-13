using System;

namespace Domain.DiaSemanaContent.Commands.Outputs
{
    public class ListarDiaSemanaResults
    {
        public string Nome { get; set; }
        public Guid DiaSemanaId { get; set; }
        public Boolean Status { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
