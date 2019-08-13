using Domain.AlunoTurmaContent.Commands.Inputs;
using Domain.AlunoTurmaContent.Repositories;
using Domain.ItemAlunoTurmaContent.Entities;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infra.Repositories
{
    public class ItemAlunoTurmaRepositorio : IItemAlunoTurmaRepositorio
    {
        private readonly DataContext _context;

        public ItemAlunoTurmaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public ItemAlunoTurma Salvar(ItemAlunoTurma obj)
        {
            var retorno = _context.ItemAlunoTurmas.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(ItemAlunoTurma obj)
        {
            _context.Entry<ItemAlunoTurma>(obj).State = EntityState.Modified;
        }

        public ItemAlunoTurma Existe(Guid alunoId,Guid turmaId)
        {
            return _context.ItemAlunoTurmas.Where(s => s.AlunoId == alunoId && s.TurmaId == turmaId).FirstOrDefault();
        }

        public DetalheAlunoTurmaResults Detalhe(Guid alunoId, Guid turmaId)
        {
            var retorno = (from i in _context.ItemAlunoTurmas
                           where i.AlunoId == alunoId && i.TurmaId == turmaId
                           select new DetalheAlunoTurmaResults()
                           {
                               AlunoId = i.AlunoId,
                               TurmaId = i.TurmaId,
                               Numero = i.Numero,
                               Status = i.Status
                           }).FirstOrDefault();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
