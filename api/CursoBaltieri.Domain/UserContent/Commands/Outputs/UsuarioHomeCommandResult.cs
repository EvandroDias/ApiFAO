namespace Domain.UserContent.Commands.Outputs
{
    public class UsuarioHomeCommandResult
    {
        public UsuarioHomeCommandResult()
        {

        }
        public UsuarioHomeCommandResult(string email, string nome, string sobreNome, string foto, string tipoUsuario)
        {
            Nome = nome;
            SobreNome = sobreNome;
            Foto = foto;
            TipoUsuario = tipoUsuario;
            Email = email;
        }

        public string Email { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Foto { get; set; }
        public string TipoUsuario { get; set; }
    }
}
