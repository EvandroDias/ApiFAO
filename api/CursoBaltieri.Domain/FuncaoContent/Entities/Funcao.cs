using Domain.FuncionarioContent.Entities;
using Domain.UserContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;
using System.Collections.Generic;

namespace Domain.FuncaoContent.Entities
{
    public class Funcao : Entity
    {
        public Funcao(string nome, Guid usuarioId)
        {
            Nome = nome;
            UsuarioId = usuarioId;
            DataCadastro = DataBrasilia.HorarioBrasilia();
            Status = true;
        }

        protected Funcao()
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
        public IEnumerable<Funcionario> Funcionario { get; private set; }

        //public IEnumerable<ItemFuncaoFuncionario> ItemFuncaoFuncionario { get; private set; }
    }
}
