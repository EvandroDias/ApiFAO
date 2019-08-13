using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using Shared.Util;
using System;

namespace Domain.ProvidenciaContent.Commands.Inputs
{
    public class SalvarProvidenciaCommands : Notifiable, IComand
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataProvidencia { get; set; }
        public Guid FuncionarioId { get; set; }
        public Guid OcorrenciaId { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(FuncionarioId.ToString(), "Providencia", "O campo FuncionarioId é obrigatório!")
                .IsNotNullOrEmpty(Titulo, "Titulo", "O campo titulo é obrigatório!")
                .IsNotNullOrEmpty(Descricao, "Descricao", "O campo Descricao é obrigatório!")
                .IsNotNullOrEmpty(OcorrenciaId.ToString(), "OcorrenciaId", "O campo ocorrenciaId é obrigatório!")
                .IsGreaterThan(DataProvidencia, DataBrasilia.HorarioBrasilia(), "DataProvidencia", "O campo data da ocorrencia deve ser maior do que a data atual!!")

            );

            return Valid;
        }

        public void SetarFuncionarioId(Guid id)
        {
            this.FuncionarioId = id;
        }
    }
}
