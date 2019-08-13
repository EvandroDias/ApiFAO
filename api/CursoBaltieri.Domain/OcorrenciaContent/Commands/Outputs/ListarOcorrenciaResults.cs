using Domain.SerieContent.Entities;
using System;

namespace Domain.OcorrenciaContent.Commands.Outputs
{
    public class ListarOcorrenciaResults
    {
        public Guid OcorrenciaId { get; set; }
        public string Titulo { get;  set; }
        public string Descricao { get;  set; }
        public DateTime DataOcorrencia { get;  set; }
        public Guid FuncionarioId { get;  set; }
        public Guid UsuarioId { get; set; }
        public Guid AlunoId { get;  set; }
        public DateTime DataCadastro { get;  set; }
        public Boolean Status { get;  set; }
        public Boolean Visualizada { get;  set; }
        public Guid SerieId { get; set; }
        public Guid TipoOcorrenciaId { get; set; }
        public string NomeAluno { get; set; }
        public string NomeSerie { get; set; }
        public string NomeTipoOcorrencia { get; set; }
        
    }
}
