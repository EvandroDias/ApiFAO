using Domain.SerieContent.Commands.Outputs;
using Domain.SerieContent.Entities;
using Domain.SerieContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class SerieRepositorio : ISerieRepositorio
    {
        private readonly DataContext _context;

        public SerieRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Serie Salvar(Serie obj)
        {
            var retorno = _context.Series.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Serie obj)
        {
            _context.Entry<Serie>(obj).State = EntityState.Modified;
        }

        public Serie Existe(Guid id)
        {
            return _context.Series.Where(s => s.Id == id).FirstOrDefault();
        }

        public Serie Existe(string nome)
        {
            return _context.Series.Where(s => s.Nome == nome).FirstOrDefault();
        }

        public DetalheSerieResults Detalhes(Guid serieId)
        {
            var retorno = (from s in _context.Series
                           where s.Id == serieId
                           select new DetalheSerieResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).FirstOrDefault();

            return retorno;
        }

        public IList<ListarSerieResults> Listar(Boolean status)
        {
            var retorno = (from s in _context.Series
                           where s.Status == status
                           select new ListarSerieResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderBy(s => s.Nome).ToList();

            return retorno;
        }

        public IList<ListarSerieResults> ListarTodos(Boolean status,int skip,int take)
        {
            var retorno = (from s in _context.Series
                           where s.Status == status
                           select new ListarSerieResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderBy(s => s.Nome).Skip(skip).Take(take).ToList();

            return retorno;
        }

        public IList<ListarSerieCmbResults> ListarCmb()
        {
            var retorno = (from s in _context.Series
                           where s.Status == true
                           select new ListarSerieCmbResults()
                           {
                               Nome = s.Nome,
                               Id = s.Id,
                              
                           }).OrderBy(s => s.Nome).ToList();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
