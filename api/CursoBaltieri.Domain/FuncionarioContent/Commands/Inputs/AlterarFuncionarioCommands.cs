using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.FuncionarioContent.Commands.Inputs
{
    public class AlterarFuncionarioCommands : Notifiable, IComand
    {
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
        public Guid Id { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                 //.IsNotNullOrEmpty(UsuarioId.ToString(), "Usuario", "O campo usuario é obrigatório!")
                .IsNotNullOrEmpty(FuncaoId.ToString(), "Função", "O campo função é obrigatório!")
                .IsNotNullOrEmpty(DadoPessoalId.ToString(), "DadoPessoalId", "O campo DadoPessoalId é obrigatório!")
                .IsNotNullOrEmpty(Nome, "Nome", "O campo nome é obrigatório!")
                .HasMinLen(Nome, 3, "Nome", "O nome deve conter no minimo 3 caracter!")
                .IsNotNullOrEmpty(SobreNome, "SobreNome", "O sobrenome é obrigatório!")
                //.IsNotNullOrEmpty(Rg, "Rg", "O rg é obrigatório!")
                .IsNotNullOrEmpty(Cpf, "Cpf", "O cpf é obrigatório!")
                .IsNotNullOrEmpty(Email, "Email", "O Email é obrigatório!")
                .IsNotNullOrEmpty(TipoUsuario, "TipoUsuario", "O TipoUsuario é obrigatório!")
                .IsNotNullOrEmpty(Celular, "Celular", "O Celular é obrigatório!")
                .IsNotNullOrEmpty(Nacionalidade, "Nacionalidade", "O Nacionalidade é obrigatório!")
                .IsNotNullOrEmpty(Uf, "Uf", "O Uf é obrigatório!")
                .IsNotNullOrEmpty(Cidade, "Cidade", "O Cidade é obrigatório!")
                .IsNotNullOrEmpty(Sexo, "Sexo", "O Sexo é obrigatório!")

            );

            return Valid;
        }
    }
}
