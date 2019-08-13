using System;

namespace Domain.ConselhoContent.Commands.Outputs
{
    public class DetalheConselhoResults
    {
        public Guid ConselhoId { get; set; }
        public string NomeAno { get; set; }
        public DateTime DataConselho { get; set; }
        public Guid FuncionarioId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid SerieId { get; set; }
        public DateTime DataCadastro { get; set; }
        public Boolean Status { get; set; }
       
       
        public string NomeSerie { get; set; }
        public string NomeFuncionario { get; set; }

        public string NomeDiretor { get; set; }
        public string NomeBimestre { get; set; }
        public Guid BimestreId { get; set; }

        public Guid AnoId { get; set; }

        public string NomeCoordenador { get; set; }
    }
}
