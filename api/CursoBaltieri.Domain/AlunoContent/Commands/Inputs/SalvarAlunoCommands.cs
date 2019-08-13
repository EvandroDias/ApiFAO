using Domain.DadoPessoalContent.Entities;
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.AlunoContent.Commands.Inputs
{
    public class SalvarAlunoCommands : Notifiable, IComand
    {
        public string Nome { get;  set; }
        public string SobreNome { get;  set; }
        
             
        public DateTime DataNascimento { get;  set; }
        public string Sexo { get;  set; }
      
        public string Nacionalidade { get;  set; }
        public string Natural { get;  set; }
        public string Email { get;  set; }
        public string Foto { get;  set; }
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

        public Guid DadoPessoalId { get;  set; }
        public Guid UsuarioId { get;  set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                
                .IsNotNullOrEmpty(UsuarioId.ToString(), "Usuario", "O campo usuario é obrigatório!")
                .IsNotNullOrEmpty(DadoPessoalId.ToString(), "DadoPessoalId", "O campo DadoPessoalId é obrigatório!")
                .IsNotNullOrEmpty(Nome, "Nome", "O campo nome é obrigatório!")
                .HasMinLen(Nome, 3, "Nome", "O nome deve conter no minimo 3 caracter!")
                .IsNotNullOrEmpty(SobreNome, "SobreNome", "O sobrenome é obrigatório!")
                .IsNotNullOrEmpty(Rm, "Rm", "O Rm é obrigatório!")
                .IsNotNullOrEmpty(Ra, "Ra", "O Ra é obrigatório!")
              
            );

            return Valid;
        }

        public void SetarUsuarioId(Guid id)
        {
            this.UsuarioId = id;
        }
    }
}
