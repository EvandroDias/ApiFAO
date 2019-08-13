using System;

namespace Domain.AlunoContent.Commands.Outputs
{
    public class DetalheAlunoResults
    {
        public Guid AlunoId { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }


        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }

        public string Nacionalidade { get; set; }
        public string Natural { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public string Rm { get; set; }
        public string Ra { get; set; }
        public string RacaCor { get; set; }
        public string Gemeos { get; set; }

        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string TipoUsuario { get; set; }

        public Guid DadoPessoalId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
