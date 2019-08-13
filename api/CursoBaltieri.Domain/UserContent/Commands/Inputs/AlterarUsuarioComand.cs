using Shared.Comands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Domain.UserContent.Commands.Inputs
{
    public class AlterarUsuarioComand : Notifiable, IComand
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public Guid UsuarioId { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
              
                .IsNotNullOrEmpty(Nome, "Nome", "O campo nome é obrigatório!")
                .IsNotNullOrEmpty(SobreNome, "SobreNome", "O campo sobreNome é obrigatório!")
                .IsNotNullOrEmpty(Email, "Email", "O campo Email é obrigatório!")
                .HasMinLen(Nome, 3, "Nome", "O nome deve conter no minimo 3 caracter!")
                .HasMaxLen(Nome, 200, "Nome", "O nome deve conter no máximo 200 caracter!")
                .HasMinLen(SobreNome, 3, "SobreNome", "O SobreNome deve conter no minimo 3 caracter!")
                .HasMaxLen(SobreNome, 200, "SobreNome", "O SobreNome deve conter no máximo 200 caracter!")
            );

            return Valid;
        }

        public void SetarUsuarioId(Guid usuarioId)
        {
            this.UsuarioId = usuarioId;
        }
    }
}
