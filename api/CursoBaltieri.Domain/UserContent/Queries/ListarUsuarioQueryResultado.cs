using Domain.UserContent.Enuns;
using System;

namespace Domain.UserContent.Queries
{
    public class ListarUsuarioQueryResultado
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Login { get;  set; }
        public string Celular { get; set; }
        public DateTime DataCadastro { get;  set; }
        public bool Status { get; set; }

    }
}
