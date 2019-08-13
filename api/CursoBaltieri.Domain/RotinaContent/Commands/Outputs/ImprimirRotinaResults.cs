using Domain.HorarioRotinaContent.Commands.Outputs;
using System.Collections.Generic;

namespace Domain.RotinaContent.Commands.Outputs
{
    public class ImprimirRotinaResults
    {
        public ImprimirRotinaResults()
        {
            this.ListarHorarioRotinaResults = new List<ListarHorarioRotinaResults>();
        }
        public ListarRotinaResults ListarRotinaResults { get; set; }
        public List<ListarHorarioRotinaResults> ListarHorarioRotinaResults { get; set; }
    }
}
