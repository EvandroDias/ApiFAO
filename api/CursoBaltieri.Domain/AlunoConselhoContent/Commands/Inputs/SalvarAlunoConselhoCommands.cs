using Domain.DadoPessoalContent.Entities;
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.AlunoConselhoContent.Commands.Inputs
{
    public class SalvarAlunoConselhoCommands : Notifiable, IComand
    {

        public string ConselhoId { get; set; }
        public Guid UsuarioId { get; set; }
        public string Descricao { get; set; }
        public string AlunoId { get; set; }



        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(AlunoId, "AlunoConselho", "O campo aluno é obrigatório!")
                .IsNotNullOrEmpty(ConselhoId, "ConselhoId", "O campo ConselhoId é obrigatório!")
                .IsNotNullOrEmpty(Descricao, "Descricao", "O campo Descricao é obrigatório!")

            );

            return Valid;
        }

        public void SetarUsuarioId(Guid id)
        {
            this.UsuarioId = id;
        }
    }
}
