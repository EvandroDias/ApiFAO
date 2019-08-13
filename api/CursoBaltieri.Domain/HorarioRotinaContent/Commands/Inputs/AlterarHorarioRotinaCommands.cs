using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.HorarioRotinaContent.Commands.Inputs
{
    public class AlterarHorarioRotinaCommands : Notifiable, IComand
    {
        public string Aula { get; set; }
        public string Conteudo { get; set; }
        public Guid DiaSemanaId { get; set; }
        public Guid HorarioRotinaId { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(DiaSemanaId.ToString(), "DiaSemanaId", "O campo dia da semana é obrigatório!")
                .IsNotNullOrEmpty(HorarioRotinaId.ToString(), "HorarioRotinaId", "O campo HorarioRotinaId é obrigatório!")
                .IsNotNullOrEmpty(Conteudo, "Conteudo", "O campo conteúdo é obrigatório!")
                .IsNotNullOrEmpty(Aula, "Aula", "O campo Aula é obrigatório!")

            );

            return Valid;
        }
    }
}
