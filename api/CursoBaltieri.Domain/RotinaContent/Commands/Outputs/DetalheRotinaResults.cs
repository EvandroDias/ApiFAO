using System;

namespace Domain.RotinaContent.Commands.Outputs
{
    public class DetalheRotinaResults
    {
        public Guid FuncionarioId { get; set; }
        public string NomeFuncionario { get; set; }
        public string ImgCabecalho { get; set; }
        public string De { get; set; }
        public string Ate { get; set; }
        public Guid SerieId { get; set; }
        public DateTime DataCadastro { get; set; }
        public Boolean Status { get; set; }
        public Boolean Visualizada { get; set; }
        public Guid RotinaId { get; set; }
    }
}
