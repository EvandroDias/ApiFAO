using Domain.FuncionarioContent.Entities;
using Domain.HorarioRotinaContent.Entities;
using Domain.SerieContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;
using System.Collections.Generic;

namespace Domain.RotinaContent.Entities
{
    public class Rotina : Entity
    {
       

        protected Rotina()
        {

        }

        public Rotina(Guid funcionarioId, string imgCabecalho, DateTime de, DateTime ate, Guid serieId)
        {
            FuncionarioId = funcionarioId;
            ImgCabecalho = imgCabecalho;
            De = de;
            Ate = ate;
            SerieId = serieId;
            DataCadastro = DataBrasilia.HorarioBrasilia();
            Status = true;
            Visualizada = false;
        }

        public void Alterar(DateTime de, DateTime ate, Guid serieId)
        {
            
            De = de;
            Ate = ate;
            SerieId = serieId;
           
        }


        public void AtivarDesativar()
        {
            Status = !Status;
        }

        public void SetarVisualizacao()
        {
            Visualizada = !Visualizada;
        }

        public Guid FuncionarioId { get; private set; }
        public string ImgCabecalho {get; private set; }
        public DateTime De { get; private set; }
        public DateTime Ate { get; private set; }
        public Guid SerieId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Boolean Status { get; private set; }
        public Boolean Visualizada { get; private set;}
     
        
        public Funcionario Funcionario { get; private set; }
        public Serie Serie { get; private set; }

        public IEnumerable<HorarioRotina> HorarioRotina { get; private set; }
    }
}
