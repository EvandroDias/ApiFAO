using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using Shared.Util;
using System;

namespace Domain.ProvidenciaContent.Commands.Inputs
{
    public class AlterarProvidenciaCommands : Notifiable, IComand
    {
        public string Titulo { get;  set; }
        public string Descricao { get;  set; }
        public DateTime DataProvidencia { get;  set; }
               
       

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Titulo, "Titulo", "O campo titulo é obrigatório!")
                .IsNotNullOrEmpty(Descricao, "Descricao", "O campo Descricao é obrigatório!")
                .IsGreaterThan(DataProvidencia,DataBrasilia.HorarioBrasilia(), "DataProvidencia", "O campo data da ocorrencia deve ser maior do que a data atual!!")

            );

            return Valid;
        }
    }
}
