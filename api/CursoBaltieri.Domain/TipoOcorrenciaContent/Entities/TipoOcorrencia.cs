using Domain.OcorrenciaContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;
using System.Collections.Generic;

namespace Domain.TipoOcorrenciaContent.Entities
{
    public class TipoOcorrencia : Entity
    {
        public TipoOcorrencia(string nome)
        {
            Nome = nome;
            DataCadastro = DataBrasilia.HorarioBrasilia();
            Status = true;
        }

        protected TipoOcorrencia()
        {

        }

        public void Alterar(string nome)
        {
            this.Nome = nome.ToUpper();
        }

        public void AtivarDesativar()
        {
            Status = !Status;
        }

        public string Nome {get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Boolean Status { get; private set; }

        
        public IEnumerable<Ocorrencia> Ocorrencia { get; private set; }

        //public IEnumerable<ItemFuncaoFuncionario> ItemFuncaoFuncionario { get; private set; }
    }
}
