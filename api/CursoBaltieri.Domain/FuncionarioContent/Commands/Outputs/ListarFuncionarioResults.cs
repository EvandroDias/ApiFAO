using System;

namespace Domain.FuncionarioContent.Commands.Outputs
{
    public class ListarFuncionarioResults
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }

        public bool Status { get; set; }

        public string Rg { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string TelefoneFixo { get; set; }
        public string Celular { get; set; }
        public string Nacionalidade { get; set; }
        public string Natural { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public string Senha { get; set; }


        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Guid DadoPessoalId { get; set; }
        public Guid FuncaoId { get; set; }
        public string TipoUsuario { get; set; }
    }
}
