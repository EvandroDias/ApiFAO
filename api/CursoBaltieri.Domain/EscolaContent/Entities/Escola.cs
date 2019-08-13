using Domain.ItemDepartamentoEscolaContent.Entities;
using Domain.TurmaContent.Entities;
using Domain.UserContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;
using System.Collections.Generic;

namespace Domain.EscolaContent.Entities
{
    public class Escola : Entity
    {
        public Escola(string nome, Guid usuarioId)
        {
            Nome = nome;
            UsuarioId = usuarioId;
            DataCadastro = DataBrasilia.HorarioBrasilia();
        }

        protected Escola()
        {

        }

        public void Alterar(string nome)
        {
            this.Nome = nome.ToUpper();
        }

        public string Nome {get; private set; }
        public Guid UsuarioId { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public Usuario Usuario { get; private set; }
        public IEnumerable<ItemDepartamentoEscola> ItemDepartamentoEscola { get; private set; }
        public IEnumerable<Turma> Turma { get; private set; }
    }
}
