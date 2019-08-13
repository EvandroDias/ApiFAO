using Shared.Comands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Domain.UserContent.Commands.Inputs
{
    public class EsqueceuSenhaComand : Notifiable, IComand
    {
        public string RecuperarSenha { get; set; }
        public string Senha { get; set; }
        public string SenhaNovamente { get; set; }

        public bool IsValid()
        {
            AddNotifications(new Contract()
           .IsNotNullOrEmpty(RecuperarSenha, "Código", "O campo código é obrigatório!")
           .IsNotNullOrEmpty(Senha, "Senha", "O campo senha é obrigatório!")
           .IsNotNullOrEmpty(SenhaNovamente, "Senha", "O campo senhaNovamente é obrigatório!")
           .AreEquals(Senha, SenhaNovamente, "SenhaNovamente","Os campos senha e senhaNovamente não são iguais!")
           );

            return Valid;
        }
    }
}
