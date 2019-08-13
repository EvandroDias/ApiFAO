using System;

namespace Domain.HorarioRotinaContent.Commands.Outputs
{
    public class DetalheHorarioRotinaResults
    {
        public string Conteudo { get;  set; }
        public Guid RotinaId { get;  set; }
        public Guid DiaSemanaId { get;  set; }
        public DateTime DataCadastro { get;  set; }
        public Boolean Status { get;  set; }
        public string Aula { get; set; }
        public Guid HorarioRotinaId { get; set; }
    }
}
