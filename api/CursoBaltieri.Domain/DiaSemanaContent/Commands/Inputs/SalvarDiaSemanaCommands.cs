using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.DiaSemanaContent.Commands.Inputs
{
    public class SalvarDiaSemanaCommands : Notifiable, IComand
    {
        public string Nome { get;  set; }
        public Guid FuncionarioId { get;  set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(FuncionarioId.ToString(), "Usuario", "O usuário é obrigatório!")
                .IsNotNullOrEmpty(Nome, "Nome", "O campo nome é obrigatório!")
                .HasMinLen(Nome, 3, "Nome", "O nome deve conter no minimo 3 caracter!")
              
            );

            return Valid;
        }

        public void SetarUsuarioId(Guid id)
        {
            this.FuncionarioId = id;
        }
    }
}
