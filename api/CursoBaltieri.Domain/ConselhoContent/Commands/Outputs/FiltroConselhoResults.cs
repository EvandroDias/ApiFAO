using System.Collections.Generic;

namespace Domain.ConselhoContent.Commands.Outputs
{
    public class FiltroConselhoResults
    {
        public List<ListarConselhoResults> ListaConselho { get; set; }
        public int Quantidade { get; set; }
    }
}
