using Domain.ItemDepartamentoEscolaContent.Entities;
using Domain.ItemDepartamentoEscolaContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infra.Repositories
{
    public class ItemDepartamentoEscolaRepositorio : IItemDepartamentoEscolaRepositorio
    {
        private readonly DataContext _context;

        public ItemDepartamentoEscolaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public ItemDepartamentoEscola Salvar(ItemDepartamentoEscola obj)
        {
            var retorno = _context.ItemDepartamentoEscolas.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(ItemDepartamentoEscola obj)
        {
            _context.Entry<ItemDepartamentoEscola>(obj).State = EntityState.Modified;
        }

        public ItemDepartamentoEscola Existe(Guid escolaId,Guid departamentoId)
        {
            return _context.ItemDepartamentoEscolas.Where(s => s.EscolaId == escolaId && s.DepartamentoId == departamentoId).FirstOrDefault();
        }

       

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
