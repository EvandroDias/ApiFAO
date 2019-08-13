using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;

namespace Domain.AlunoTurmaContent.Commands.Inputs
{
    public class SalvarItemAlunoTurmaCommands : Notifiable, IComand
    {
        public string AlunoId { get; set; }
        public string TurmaId { get; set; }
        public string Status { get; set; }
        public int Numero { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(AlunoId, "AlunoId", "O aluno é obrigatório!")
                .IsNotNullOrEmpty(TurmaId, "TurmaId", "A turma é obrigatória!")
                .IsGreaterThan(Numero,0, "Numero", "o Número é obrigatória!")
            );

            return Valid;
        }


    }
}
