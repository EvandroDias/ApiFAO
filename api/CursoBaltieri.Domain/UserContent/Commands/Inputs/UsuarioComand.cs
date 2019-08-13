using System;


namespace Domain.UserContent.Commands.Inputs
{
    public class UsuarioComand
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Foto { get; set; }
        public string TipoUsuario { get; set; }
        public bool ConfigDadoPessoal { get; set; }

    }
}
