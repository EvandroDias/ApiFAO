using Domain.AlunoContent.Entities;
using Domain.ProvidenciaContent.Entities;
using Domain.SerieContent.Entities;
using Domain.TipoOcorrenciaContent.Entities;
using Domain.UserContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;
using System.Collections.Generic;

namespace Domain.OcorrenciaContent.Entities
{
    public class Ocorrencia : Entity
    {
       

        protected Ocorrencia()
        {

        }

        public Ocorrencia(string titulo, string descricao, DateTime dataOcorrencia, Guid usuarioId, Guid alunoId,string periodo,Guid serieId,Guid tipoOcorrenciaId,Guid funcionarioId)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            UsuarioId = usuarioId;
            AlunoId = alunoId;
            DataCadastro = DataBrasilia.HorarioBrasilia();
            Status = true;
            Visualizada = false;
            Periodo = periodo;
            SerieId = serieId;
            TipoOcorrenciaId = tipoOcorrenciaId;
            FuncionarioId = funcionarioId;
        }

        public void Alterar(string titulo, string descricao, DateTime dataOcorrencia, Guid alunoId, string periodo, Guid serieId, Guid tipoOcorrenciaId, Guid funcionarioId)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            AlunoId = alunoId;
            Periodo = periodo;
            SerieId = serieId;
            TipoOcorrenciaId = tipoOcorrenciaId;
            FuncionarioId = funcionarioId;
        }


        public void AtivarDesativar()
        {
            Status = !Status;
        }

        public void SetarVisualizacao()
        {
            Visualizada = !Visualizada;
        }

        public string Titulo {get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataOcorrencia { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid TipoOcorrenciaId { get; private set; }
        public Guid SerieId { get; private set; }
        public Guid AlunoId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Boolean Status { get; private set; }
        public Boolean Visualizada { get; private set; }
        public string Periodo { get; private set; }
        public Guid FuncionarioId { get; private set; }


        public Usuario Usuario { get; private set; }
        public Aluno Aluno { get; private set; }
        public TipoOcorrencia TipoOcorrencia { get;private set; }
        public Serie Serie { get; private set; }

        public IEnumerable<Providencia> Providencia { get; private set; }

    }
}
