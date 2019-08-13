using Domain.AlunoContent.Entities;
using Domain.FuncionarioContent.Entities;
using Domain.OcorrenciaContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;

namespace Domain.ProvidenciaContent.Entities
{
    public class Providencia : Entity
    {
       

        protected Providencia()
        {

        }

        public Providencia(string titulo, string descricao, DateTime dataProvidencia, Guid funcionarioId, Guid ocorrenciaId)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataProvidencia = dataProvidencia;
            FuncionarioId = funcionarioId;
            OcorrenciaId = ocorrenciaId;
            DataCadastro = DataBrasilia.HorarioBrasilia();
            Status = true;
        }

        public void Alterar(string titulo, string descricao, DateTime dataProvidencia)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataProvidencia = dataProvidencia;
        }


        public void AtivarDesativar()
        {
            Status = !Status;
        }

        public string Titulo {get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataProvidencia { get; private set; }
        public Guid FuncionarioId { get; private set; }
        public Guid OcorrenciaId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Boolean Status { get; private set; }

        public Funcionario Funcionario { get; private set; }
        public Ocorrencia Ocorrencia { get; private set; }
    }
}
