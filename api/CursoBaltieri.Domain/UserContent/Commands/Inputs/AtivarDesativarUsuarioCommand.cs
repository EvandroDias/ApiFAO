using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.UserContent.Commands.Inputs
{
    public class AtivarDesativarUsuarioCommand : Notifiable, IComand
    {
        public Guid Id { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()

                .IsNotNullOrEmpty(Id.ToString(), "Id", "O campo Id é obrigatório!")
              
            );

            return Valid;
        }
    }
}
