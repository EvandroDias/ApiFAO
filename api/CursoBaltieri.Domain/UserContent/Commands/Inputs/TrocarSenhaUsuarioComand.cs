using Shared.Comands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Domain.UserContent.Commands.Inputs
{
    public class TrocarSenhaUsuarioComand : Notifiable, IComand
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string SenhaNova { get; set; }
        public string ChaveSenha { get; set; }
        public Guid UsuarioId { get; set; }

        public bool IsValid()
        {
            AddNotifications(new Contract()
                //.IsEmail(Login, "Login", "O E-mail é inválido")
               .IsNotNullOrEmpty(Login, "Login", "O Login é obrigatório")
               .IsNotNullOrEmpty(Senha, "Senha", "A senha é obrigatória")
               .IsNotNullOrEmpty(SenhaNova, "SenhaNova", "A senha nova é obrigatória")
              );

            return Valid;
        }

        public void SetarUsuarioId(Guid usuarioId)
        {
            this.UsuarioId = usuarioId;
        }
    }
}
