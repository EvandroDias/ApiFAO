using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.RotinaContent.Commands.Outputs
{
    public class FiltroRotinaResults
    {
        public List<ListarRotinaResults> ListaRotina { get; set; }
        public int Quantidade { get; set; }
    }
}
