using Domain.DiaSemanaContent.Commands.Outputs;
using Domain.DiaSemanaContent.Entities;
using Domain.DiaSemanaContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class DiaSemanaRepositorio : IDiaSemanaRepositorio
    {
        private readonly DataContext _context;

        public DiaSemanaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public DiaSemana Salvar(DiaSemana obj)
        {
            var retorno = _context.DiaSemanas.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(DiaSemana obj)
        {
            _context.Entry<DiaSemana>(obj).State = EntityState.Modified;
        }

        public DiaSemana Existe(Guid id)
        {
            return _context.DiaSemanas.Where(s => s.Id == id).FirstOrDefault();
        }

        public DetalheDiaSemanaResults Detalhes(Guid serieId)
        {
            var retorno = (from s in _context.DiaSemanas
                           where s.Id == serieId
                           select new DetalheDiaSemanaResults()
                           {
                               Nome = s.Nome,
                               Id = s.Id,
                               Status = s.Status
                           }).FirstOrDefault();

            return retorno;
        }

        public IList<ListarDiaSemanaResults> ListarTodos()
        {
            var retorno = (from s in _context.DiaSemanas
                           select new ListarDiaSemanaResults()
                           {
                               Nome = s.Nome,
                               DiaSemanaId = s.Id,
                               Status = s.Status,
                               DataCadastro = s.DataCadastro
                               
                           }).OrderBy(s => s.DataCadastro).ToList();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
