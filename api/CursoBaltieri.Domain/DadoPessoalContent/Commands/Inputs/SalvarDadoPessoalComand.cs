using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.DadoPessoalContent.Commands.Inputs
{
    public class SalvarDadoPessoalComand : Notifiable, IComand
    {
        public string Rua { get;  set; }
        public string Numero { get;  set; }
        public string Bairro { get;  set; }
        public string Complemento { get;  set; }
        public string Uf { get;  set; }
        public string Cidade { get;  set; }
        public string Cep { get;  set; }
        public float Latitude { get;  set; }
        public float Longitude { get;  set; }
        public string TelefoneFixo { get;  set; }
        public string Celular { get;  set; }
        public string WattsApp { get;  set; }
        public Guid UsuarioId { get; set; }

        public bool IsValid()
        {
            AddNotifications(new Contract()
             .IsNotNullOrEmpty(UsuarioId.ToString(), "Usuario", "Não foi possível encontrar o usuário,tente novamente!")
             .IsNotNullOrEmpty(Rua, "Rua", "O campo rua é obrigatória!")
             .IsNotNullOrEmpty(Numero, "Numero","O campo número é obrigatório!")
             .IsNotNullOrEmpty(Bairro, "Bairro","O campo bairro é obrigatório!")
             .IsNotNullOrEmpty(Cep, "Cep", "O campo cep é obrigatório!")
             .IsNotNullOrEmpty(Cidade, "Cidade", "O campo cidade é obrigatório!")
             .IsNotNullOrEmpty(Uf, "Uf", "O campo uf é obrigatório!")
             .IsNotNullOrEmpty(Celular, "Celular", "O campo celular é obrigatório!")
             .HasMinLen(Cep, 9, "Cep", "O campo cep deve conter no minimo 8 caracter")
             .HasMaxLen(Cep, 9, "Cep", "O campo cep deve conter no máximo 8 caracter")
             .HasMaxLen(Celular, 14, "Celular", "O campo celular deve conter no máximo 14 caracter")
         );

            return Valid;
        }

        public void SetarUsuarioId(Guid usuarioId)
        {
            this.UsuarioId = usuarioId;
        }
    }
}
