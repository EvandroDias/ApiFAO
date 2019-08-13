
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.FuncionarioContent.Commands.Inputs
{
    public class AtivarDesativarCommands : Notifiable, IComand
    {
        public Guid Id { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
               
                .IsNotNullOrEmpty(Id.ToString(), "Id", "O id do é obrigatório!")
             
            );

            return Valid;
        }
    }
}
