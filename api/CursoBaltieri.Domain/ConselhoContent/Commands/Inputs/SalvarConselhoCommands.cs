using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.ConselhoContent.Commands.Inputs
{
    public class SalvarConselhoCommands : Notifiable, IComand
    {
        
       
        public DateTime DataConselho { get; set; }
        public string FuncionarioId { get; set; }
        public Guid UsuarioId { get; set; }
        public string BimestreId { get; set; }
        public string SerieId { get; set; }

        public string NomeCoordenador { get; set; }

        public string NomeDiretor { get; set; }

        public string AnoId { get; set; }


        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(FuncionarioId, "Conselho", "O campo FuncionarioId é obrigatório!")
                .IsNotNullOrEmpty(NomeCoordenador, "NomeCoordenador", "O campo NomeCoordenador é obrigatório!")
                .IsNotNullOrEmpty(NomeDiretor, "NomeDiretor", "O campo NomeDiretor é obrigatório!")
                .IsNotNullOrEmpty(SerieId, "SerieId", "O campo SerieId é obrigatório!")
                .IsNotNullOrEmpty(AnoId, "AnoId", "O campo AnoId é obrigatório!")
                .IsNotNullOrEmpty(BimestreId, "BimestreId", "O campo BimestreId é obrigatório!")
                .IsNotNull(DataConselho, "DataConselho", "O campo data da ocorrencia deve ser maior do que a data atual!!")

            );

            return Valid;
        }

        public void SetarUsuarioId(Guid id)
        {
            this.UsuarioId = id;
        }
    }
}
