using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.ItemDepartamentoEscolaContent.Commands.Inputs
{
    public class AlterarItemDepartamentoEscolaCommands : Notifiable, IComand
    {
        public Guid EscolaId { get; set; }
        public Guid DepartamentoId { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(EscolaId.ToString(), "EscolaId", "A EscolaId é obrigatório!")
                .IsNotNullOrEmpty(DepartamentoId.ToString(), "DepartamentoId", "O DepartamentoId é obrigatório!")

            );

            return Valid;
        }
    }
}
