using Domain.DiaSemanaContent.Entities;
using Domain.RotinaContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;

namespace Domain.HorarioRotinaContent.Entities
{
    public class HorarioRotina : Entity
    {
        protected HorarioRotina()
        {

        }

        public HorarioRotina(string conteudo,string aula, Guid rotinaId, Guid diaSemanaId)
        {
            Conteudo = conteudo;
            RotinaId = rotinaId;
            DiaSemanaId = diaSemanaId;
            DataCadastro = DataBrasilia.HorarioBrasilia();
            Aula = aula;
            Status = true;
        }

        public void Alterar(string conteudo,Guid diaSemanaId)
        {
            Conteudo = conteudo;
            DiaSemanaId = diaSemanaId;
        }

        public void AtivarDesativar()
        {
            Status = !Status;
        }

        public string Conteudo {get; private set; }
        public string Aula { get; private set; }
        public Guid RotinaId { get; private set; }
        public Guid DiaSemanaId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Boolean Status { get; private set; }

        public DiaSemana DiaSemana { get; private set; }
        public Rotina Rotina { get; private set; }



    }
}
