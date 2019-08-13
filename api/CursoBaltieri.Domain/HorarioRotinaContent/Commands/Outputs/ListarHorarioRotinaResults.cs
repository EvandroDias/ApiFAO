using System;

namespace Domain.HorarioRotinaContent.Commands.Outputs
{
    public class ListarHorarioRotinaResults
    {
        public Guid HorarioRotinaId { get; set; }
        public string Conteudo { get;  set; }
        public Guid RotinaId { get;  set; }
        public Guid DiaSemanaId { get;  set; }
        public string NomeSemana { get; set; }
        public DateTime DataCadastro { get;  set; }
        public Boolean Status { get;  set; }
        public string Aula { get; set; }
        public int Order { get; set; }
    }
}
