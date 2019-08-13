using Domain.BimestreContent.Commands.Outputs;
using Domain.BimestreContent.Entities;
using Domain.BimestreContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class BimestreRepositorio : IBimestreRepositorio
    {
        private readonly DataContext _context;

        public BimestreRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Bimestre Salvar(Bimestre obj)
        {
            var retorno = _context.Bimestres.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Bimestre obj)
        {
            _context.Entry<Bimestre>(obj).State = EntityState.Modified;
        }

        public Bimestre Existe(Guid id)
        {
            return _context.Bimestres.Where(s => s.Id == id).FirstOrDefault();
        }

        public DetalheBimestreResults Detalhes(Guid serieId)
        {
            var retorno = (from s in _context.Bimestres
                           where s.Id == serieId
                           select new DetalheBimestreResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).FirstOrDefault();

            return retorno;
        }
        public IList<ListarBimestreResults> Listar(Boolean status)
        {
            var retorno = (from s in _context.Bimestres
                           where s.Status == status
                           select new ListarBimestreResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderBy(s => s.Nome).ToList();

            return retorno;
        }

        public IList<ListarBimestreResults> ListarTodos(Boolean status,int skip,int take)
        {
            var retorno = (from s in _context.Bimestres
                           where s.Status == status
                           select new ListarBimestreResults()
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
