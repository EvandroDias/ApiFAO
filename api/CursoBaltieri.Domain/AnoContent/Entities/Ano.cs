using Domain.ConselhoContent.Entities;
using Domain.FuncionarioContent.Entities;
using Domain.TurmaContent.Entities;
using Domain.UserContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;
using System.Collections.Generic;

namespace Domain.AnoContent.Entities
{
    public class Ano : Entity
    {
        public Ano(string nome, Guid usuarioId)
        {
            Nome = nome;
            UsuarioId = usuarioId;
            DataCadastro = DataBrasilia.HorarioBrasilia();
            Status = true;
        }

        protected Ano()
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

        public Usuario Usuario { get; private set; }
        public IEnumerable<Conselho> Conselho { get; private set; }

        public IEnumerable<Turma> Turma { get; private set; }
    }
}
