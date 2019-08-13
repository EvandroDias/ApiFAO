using System;

namespace Domain.DepartamentoContent.Commands.Outputs
{
    public class ListarDepartamentoResults
    {
        public string Nome { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid Id { get; set; }
        public Boolean Status { get; set; }
    }
}
