using Domain.OcorrenciaContent.Entities;
using Domain.TurmaContent.Entities;
using Domain.UserContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;
using System.Collections.Generic;

namespace Domain.SerieContent.Entities
{
    public class Serie : Entity
    {
        public Serie(string nome,int numero, Guid usuarioId)
        {
            Nome = nome;
            UsuarioId = usuarioId;
            DataCadastro = DataBrasilia.HorarioBrasilia();
            Status = true;
            Numero = numero;
        }

        protected Serie()
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
        public Guid UsuarioId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Boolean Status { get; private set; }

        public int Numero { get; private set; }
        public Usuario Usuario { get; private set; }

        public IEnumerable<Ocorrencia> Ocorrencia { get; private set; }
        public IEnumerable<Turma> Turma { get; private set; }
    }
}
