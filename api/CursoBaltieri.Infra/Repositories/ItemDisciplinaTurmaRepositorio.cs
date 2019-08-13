using Domain.DisciplinaTurmaContent.Repositories;
using Domain.ItemDisciplinaTurmaContent.Entities;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infra.Repositories
{
    public class ItemDisciplinaTurmaRepositorio : IItemDisciplinaTurmaRepositorio
    {
        private readonly DataContext _context;

        public ItemDisciplinaTurmaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public ItemDisciplinaTurma Salvar(ItemDisciplinaTurma obj)
        {
            var retorno = _context.ItemDisciplinaTurmas.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(ItemDisciplinaTurma obj)
        {
            _context.Entry<ItemDisciplinaTurma>(obj).State = EntityState.Modified;
        }

        public ItemDisciplinaTurma Existe(Guid alunoId,Guid turmaId)
        {
            return _context.ItemDisciplinaTurmas.Where(s => s.DisciplinaId == alunoId && s.TurmaId == turmaId).FirstOrDefault();
        }

       

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
