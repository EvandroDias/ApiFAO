using Domain.FuncaoContent.Commands.Outputs;
using Domain.FuncaoContent.Entities;
using Domain.FuncaoContent.Repositories;
using Domain.TipoOcorrenciaContent.Commands.Outputs;
using Domain.TipoOcorrenciaContent.Entities;
using Domain.TipoOcorrenciaContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class TipoOcorrenciaRepositorio : ITipoOcorrenciaRepositorio
    {
        private readonly DataContext _context;

        public TipoOcorrenciaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public TipoOcorrencia Salvar(TipoOcorrencia obj)
        {
            var retorno = _context.TipoOcorrencias.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(TipoOcorrencia obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public TipoOcorrencia Existe(Guid id)
        {
            return _context.TipoOcorrencias.Where(s => s.Id == id).FirstOrDefault();
        }

        public DetalheTipoOcorrenciaResults Detalhes(Guid tipoOcorrenciaId)
        {
            var retorno = (from s in _context.TipoOcorrencias
                           where s.Id == tipoOcorrenciaId
                           select new DetalheTipoOcorrenciaResults()
                           {
                               Nome = s.Nome,
                               Id = s.Id,
                               Status = s.Status
                           }).FirstOrDefault();

            return retorno;
        }

        public IList<ListarTipoOcorrenciaResults> ListarTodos()
        {
            var retorno = (from s in _context.TipoOcorrencias
                           where s.Status == true
                           select new ListarTipoOcorrenciaResults()
                           {
                               Nome = s.Nome,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderByDescending(s => s.Nome).ToList();

            return retorno;
        }

        public IList<ListarTipoOcorrenciaResults> ListarTodos(Boolean status,int skip,int take)
        {
            var retorno = (from s in _context.TipoOcorrencias
                           where s.Status == status
                           select new ListarTipoOcorrenciaResults()
                           {
                               Nome = s.Nome,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderBy(s => s.Nome).Skip(skip).Take(take).ToList();

            return retorno;
        }

        public IList<ListarTipoOcorrenicaCmbResults> ListarCmb()
        {
            var retorno = (from s in _context.TipoOcorrencias
                           where s.Status == true
                           select new ListarTipoOcorrenicaCmbResults()
                           {
                               Nome = s.Nome,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderByDescending(s => s.Nome).ToList();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
