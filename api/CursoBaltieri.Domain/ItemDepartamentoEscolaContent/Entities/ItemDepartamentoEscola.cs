using Domain.DepartamentoContent.Entities;
using Domain.EscolaContent.Entities;
using FluentValidator;
using Shared.Util;
using System;

namespace Domain.ItemDepartamentoEscolaContent.Entities
{
    public class ItemDepartamentoEscola :  Notifiable
    {
        public ItemDepartamentoEscola(Guid escolaId, Guid departamentoId)
        {
            EscolaId = escolaId;
            DepartamentoId = departamentoId;
            DataCadastro = DataBrasilia.HorarioBrasilia();
        }

        protected ItemDepartamentoEscola()
        {

        }

        public void Alterar(Guid escolaId, Guid departamentoId)
        {
            this.EscolaId = escolaId;
            this.EscolaId = departamentoId;
        }

        public Guid EscolaId {get; private set; }
        public Guid DepartamentoId { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public Escola Escola { get; private set; }
        public Departamento Departamento { get; private set; }
    }
}
