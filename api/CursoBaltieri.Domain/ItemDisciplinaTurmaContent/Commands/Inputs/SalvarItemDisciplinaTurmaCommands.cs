using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;

namespace Domain.DisciplinaTurmaContent.Commands.Inputs
{
    public class SalvarDisciplinaTurmaCommands : Notifiable, IComand
    {
        public string DisciplinaId { get; set; }
        public string TurmaId { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(DisciplinaId, "DisciplinaId", "A disciplina é obrigatória!")
                .IsNotNullOrEmpty(TurmaId, "TurmaId", "A turma é obrigatória!")

            );

            return Valid;
        }


    }
}
