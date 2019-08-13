using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.AlunoConselhoContent.Commands.Inputs
{
    public class AlterarAlunoConselhoCommands : Notifiable, IComand
    {
        public Guid AlunoConselhoId { get; set; }
        public string ConselhoId { get; set; }
        public string UsuarioId { get;  set; }
        public string Descricao { get;  set; }
        public string AlunoId { get;  set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(AlunoId, "AlunoConselho", "O campo aluno é obrigatório!")
                .IsNotNullOrEmpty(ConselhoId, "ConselhoId", "O campo ConselhoId é obrigatório!")
                .IsNotNullOrEmpty(Descricao, "Descricao", "O campo Descricao é obrigatório!")

            );

            return Valid;
        }
    }
}
