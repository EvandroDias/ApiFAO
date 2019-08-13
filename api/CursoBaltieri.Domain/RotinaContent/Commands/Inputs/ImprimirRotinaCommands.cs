using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.RotinaContent.Commands.Inputs
{
    public class ImprimirRotinaCommands : Notifiable, IComand
    {
        public Guid RotinaId { get;  set; }
       

        public bool IsValid()
        {
            AddNotifications(new Contract()

                 .IsNotNullOrEmpty(RotinaId.ToString(), "RotinaId", "O campo RotinaId é obrigatório!")
                 

             );

            return Valid;
        }

       
       
    }
}
