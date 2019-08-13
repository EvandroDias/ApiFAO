using System;

namespace Domain.TurmaContent.Commands.Outputs
{
    public class ListarTurmaResults
    {
        public Guid TurmaId { get; set; }
        public string Ensino { get; set; }
        public string UsuarioId { get; set; }
        public string SerieId { get; set; }
        public string NomeSerie { get; set; }
        public string DepartamentoId { get; set; }
        public string NomeSala { get; set; }
        public string AnoId { get; set; }
        public string NomeAno { get; set; }
        public string EscolaId { get; set; }
        public string NomeEscola { get; set; }
        public string Periodo { get; set; }
        public string Coordenador { get; set; }
        public string Diretor { get; set; }
        public string FuncionarioId { get; set; }
        public string NomeProfessor { get; set; }
        public int QtdAulas1Bimestre { get; set; }
        public int QtdAulas2Bimestre { get; set; }
        public int QtdAulas3Bimestre { get; set; }
        public int QtdAulas4Bimestre { get; set; }
    }
}
