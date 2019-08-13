using Domain.HorarioRotinaContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;
using System.Collections.Generic;

namespace Domain.DiaSemanaContent.Entities
{
    public class DiaSemana : Entity
    {
        public DiaSemana(string nome)
        {
            Nome = nome;
            DataCadastro = DataBrasilia.HorarioBrasilia();
            Status = true;
        }

        protected DiaSemana()
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
        public int Order { get; private set;}

        public IEnumerable<HorarioRotina> HorarioRotina { get; private set; }

    }
}
