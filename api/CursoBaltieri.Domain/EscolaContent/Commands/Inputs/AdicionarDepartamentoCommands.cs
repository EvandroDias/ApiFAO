using Domain.ItemDepartamentoEscolaContent.Commands.Inputs;
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;
using System.Collections.Generic;

namespace Domain.EscolaContent.Commands.Inputs
{
    public class AdicionarDepartamentoCommands : Notifiable, IComand
    {
        public Guid EscolaId { get; set; }
        public IList<ItemDepartamentoEscolaCommands> DepartamentoId { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(EscolaId.ToString(), "EscolaId", "A EscolaId é obrigatório!")
                .IsNotNull(DepartamentoId, "DepartamentoId", "O DepartamentoId é obrigatório!")

            );

            return Valid;
        }
    }
}
