using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.HorarioRotinaContent.Commands.Inputs
{
    public class SalvarHorarioRotinaCommands : Notifiable, IComand
    {
       
        public string Conteudo { get;  set; }
        public Guid RotinaId { get;  set; }
        public Guid DiaSemanaId { get;  set; }
        public string Aula { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(RotinaId.ToString(), "RotinaId", "O RotinaId é obrigatório!")
                .IsNotNullOrEmpty(Conteudo, "Conteudo", "O campo Conteudo é obrigatório!")
                .IsNotNullOrEmpty(Aula, "Aula", "O campo Aula é obrigatório!")
                .IsNotNullOrEmpty(DiaSemanaId.ToString(), "DiaSemanaId", "O campo DiaSemanaId é obrigatório!")

            );

            return Valid;
        }

       
    }
}
