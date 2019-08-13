using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.TipoOcorrenciaContent.Commands.Inputs
{
    public class SalvarTipoOcorrenciaCommands : Notifiable, IComand
    {
        public string Nome { get;  set; }
      
        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, "Nome", "O campo nome é obrigatório!")
                .HasMinLen(Nome, 3, "Nome", "O nome deve conter no minimo 3 caracter!")
              
            );

            return Valid;
        }

     
    }
}
