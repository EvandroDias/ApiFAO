using System;

namespace Domain.FuncaoContent.Commands.Outputs
{
    public class ListarFuncaoResults
    {
        public string Nome { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid Id { get; set; }
        public Boolean Status { get; set; }
    }
}
