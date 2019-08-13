using Domain.FuncaoContent.Commands.Outputs;
using Domain.FuncaoContent.Entities;
using Domain.FuncaoContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class FuncaoRepositorio : IFuncaoRepositorio
    {
        private readonly DataContext _context;

        public FuncaoRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Funcao Salvar(Funcao obj)
        {
            var retorno = _context.Funcaos.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Funcao obj)
        {
            _context.Entry<Funcao>(obj).State = EntityState.Modified;
        }

        public Funcao Existe(Guid id)
        {
            return _context.Funcaos.Where(s => s.Id == id).FirstOrDefault();
        }

        public DetalheFuncaoResults Detalhes(Guid serieId)
        {
            var retorno = (from s in _context.Funcaos
                           where s.Id == serieId
                           select new DetalheFuncaoResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).FirstOrDefault();

            return retorno;
        }
        public IList<ListarFuncaoResults> Listar(Boolean status)
        {
            var retorno = (from s in _context.Funcaos
                           where s.Status == status
                           select new ListarFuncaoResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderBy(s => s.Nome).ToList();

            return retorno;
        }

        public IList<ListarFuncaoResults> ListarTodos(Boolean status,int skip,int take)
        {
            var retorno = (from s in _context.Funcaos
                           where s.Status == status
                           select new ListarFuncaoResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderBy(s => s.Nome).Skip(skip).Take(take).ToList();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
