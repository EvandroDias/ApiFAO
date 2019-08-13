using Domain.AnoContent.Commands.Outputs;
using Domain.AnoContent.Entities;
using Domain.AnoContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class AnoRepositorio : IAnoRepositorio
    {
        private readonly DataContext _context;

        public AnoRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Ano Salvar(Ano obj)
        {
            var retorno = _context.Anos.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Ano obj)
        {
            _context.Entry<Ano>(obj).State = EntityState.Modified;
        }

        public Ano Existe(Guid id)
        {
            return _context.Anos.Where(s => s.Id == id).FirstOrDefault();
        }
        public Ano Existe(string nome)
        {
            return _context.Anos.Where(s => s.Nome == nome).FirstOrDefault();
        }

        public DetalheAnoResults Detalhes(Guid serieId)
        {
            var retorno = (from s in _context.Anos
                           where s.Id == serieId
                           select new DetalheAnoResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).FirstOrDefault();

            return retorno;
        }
        public IList<ListarAnoResults> Listar(Boolean status)
        {
            var retorno = (from s in _context.Anos
                           where s.Status == status
                           select new ListarAnoResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderBy(s => s.Nome).ToList();

            return retorno;
        }

        public IList<ListarAnoResults> ListarTodos(Boolean status,int skip,int take)
        {
            var retorno = (from s in _context.Anos
                           where s.Status == status
                           select new ListarAnoResults()
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
