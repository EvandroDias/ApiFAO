using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using Shared.Util;
using System;

namespace Domain.OcorrenciaContent.Commands.Inputs
{
    public class SalvarOcorrenciaCommands : Notifiable, IComand
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public Guid FuncionarioId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid AlunoId { get; set; }
        public Guid TipoOcorrenciaId { get; set; }
        public Guid SerieId { get; set; }
        public string Periodo { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(FuncionarioId.ToString(), "Ocorrencia", "O campo FuncionarioId é obrigatório!")
                .IsNotNullOrEmpty(UsuarioId.ToString(), "UsuarioId", "O campo UsuarioId é obrigatório!")
                .IsNotNullOrEmpty(Titulo, "Titulo", "O campo Titulo é obrigatório!")
                .IsNotNullOrEmpty(SerieId.ToString(), "SerieId", "O campo SerieId é obrigatório!")
                .IsNotNullOrEmpty(Periodo, "Periodo", "O campo Periodo é obrigatório!")
                .IsNotNullOrEmpty(AlunoId.ToString(), "AlunoId", "O campo AlunoId é obrigatório!")
                .IsNotNull(DataOcorrencia, "DataOcorrencia", "O campo data da ocorrencia deve ser maior do que a data atual!!")

            );

            return Valid;
        }

        public void SetarUsuarioId(Guid id)
        {
            this.UsuarioId = id;
        }
    }
}
