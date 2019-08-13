using Shared.Comands;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.UserContent.Commands.Inputs
{
    public class LoginUsuarioComand : Notifiable, IComand
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public bool IsValid()
        {
            AddNotifications(new Contract()
                .IsEmail(Email, "Email", "O E-mail é inválido")
                .IsEmailOrEmpty(Email, "Email", "O E-mail é obrigatório")
               .IsEmailOrEmpty(Senha, "Senha", "A senha é obrigatória")
              );

            return Valid;
        }
    }
}
