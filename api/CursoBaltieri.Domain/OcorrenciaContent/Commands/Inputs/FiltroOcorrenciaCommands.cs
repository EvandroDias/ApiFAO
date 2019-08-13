
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.OcorrenciaContent.Commands.Inputs
{
    public class FiltroOcorrenciaCommands : Notifiable, IComand
    {
        public string AlunoId { get; set; }
        public string SerieId { get; set; }
        public string TipoOcorrenciaId { get; set; }
        public string FuncionarioId { get; set; }
        public string De { get; set; }
        public string Ate { get; set; }
        public string TipoFiltro { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Dias { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(TipoOcorrenciaId, "TipoOcorrenciaId", "O campo TipoOcorrenciaId é obrigatório!")
                .IsNotNullOrEmpty(De, "De", "O campo De é obrigatório!")
                .IsNotNullOrEmpty(Ate, "Ate", "O campo Ate é obrigatório!")
                .IsNotNullOrEmpty(TipoFiltro, "TipoFiltro", "O campo TipoFiltro é obrigatório!")

            );

            return Valid;
        }
    }
}
