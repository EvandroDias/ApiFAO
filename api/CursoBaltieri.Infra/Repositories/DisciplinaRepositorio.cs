using Domain.DisciplinaContent.Commands.Outputs;
using Domain.SerieContent.Entities;
using Domain.SerieContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class DisciplinaRepositorio : IDisciplinaRepositorio
    {
        private readonly DataContext _context;

        public DisciplinaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Disciplina Salvar(Disciplina obj)
        {
            var retorno = _context.Disciplinas.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Disciplina obj)
        {
            _context.Entry<Disciplina>(obj).State = EntityState.Modified;
        }

        public Disciplina Existe(Guid id)
        {
            return _context.Disciplinas.Where(s => s.Id == id).FirstOrDefault();
        }

        public DetalheDisciplinaResults Detalhes(Guid serieId)
        {
            var retorno = (from s in _context.Disciplinas
                           where s.Id == serieId
                           select new DetalheDisciplinaResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).FirstOrDefault();

            return retorno;
        }

        public IList<ListarDisciplinaResults> ListarTodos()
        {
            var retorno = (from s in _context.Disciplinas
                           where s.Status == true
                           select new ListarDisciplinaResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderByDescending(s => s.Nome).ToList();

            return retorno;
        }

        public IList<ListarDisciplinaResults> ListarTodos(Boolean status,int skip,int take)
        {
            var retorno = (from s in _context.Disciplinas
                           where s.Status == status
                           select new ListarDisciplinaResults()
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
