using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.SerieContent.Commands.Inputs
{
    public class SalvarSerieCommands : Notifiable, IComand
    {
        public string Nome { get;  set; }
        public int Numero { get; set; }
        public Guid UsuarioId { get;  set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(UsuarioId.ToString(), "Usuario", "O usuário é obrigatório!")
                .IsNotNullOrEmpty(Nome, "Nome", "O campo nome é obrigatório!")
                .HasMinLen(Nome, 3, "Nome", "O nome deve conter no minimo 3 caracter!")
              
            );

            return Valid;
        }

        public void SetarUsuarioId(Guid id)
        {
            this.UsuarioId = id;
        }
    }
}
