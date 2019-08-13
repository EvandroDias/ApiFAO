using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.RotinaContent.Commands.Inputs
{
    public class AlterarRotinaCommands : Notifiable, IComand
    {
        
        public DateTime De { get;  set; }
        public DateTime Ate { get;  set; }
        public Guid SerieId { get;  set; }
        public Guid RotinaId { get; set; }
        public Guid UsuarioId { get; set; }

        public bool IsValid()
        {
            AddNotifications(new Contract()
                                 
                 .IsNotNullOrEmpty(SerieId.ToString(), "SerieId", "O campo SerieId é obrigatório!")

             );

            return Valid;
        }

        public void SetarUsuarioId(Guid id)
        {
            this.UsuarioId = id;
        }

       
    }
}
