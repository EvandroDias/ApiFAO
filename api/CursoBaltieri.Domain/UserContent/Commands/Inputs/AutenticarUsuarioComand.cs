using Shared.Comands;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.UserContent.Commands.Inputs
{
    public class AutenticarUsuarioComand : Notifiable, IComand
    {
        //public AutenticarUsuarioComand()
        //{
        //    this.IsValid();
        //}
       
        public string Login { get; set; }
        public string Senha { get; set; }

        public bool IsValid()
        {
            AddNotifications(new Contract()
                //.IsEmail(Email, "Email", "Digite um  e-mail válido")
                .IsNotNullOrEmpty(Login, "Login", "O Login é obrigatório")
               .IsNotNullOrEmpty(Senha, "Senha", "A senha é obrigatória")
              );

            return Valid;
        }
    }
}
