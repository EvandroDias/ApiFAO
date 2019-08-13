
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;

namespace Domain.ConselhoContent.Commands.Inputs
{
    public class FiltroConselhoCommands : Notifiable, IComand
    {
        public string ConselhoId { get; set; }
        public string AlunoId { get; set; }
        public string AnoId { get; set; }
        public string SerieId { get; set; }
        public string BimestreId { get; set; }
        public string FuncionarioId { get; set; }
        public string De { get; set; }
        public string Ate { get; set; }
        public string TipoFiltro { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Dias { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()

                

            );

            return Valid;
        }
    }
}
