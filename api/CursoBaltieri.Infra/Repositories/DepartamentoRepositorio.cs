using Domain.DepartamentoContent.Commands.Outputs;
using Domain.DepartamentoContent.Entities;
using Domain.DepartamentoContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class DepartamentoRepositorio : IDepartamentoRepositorio
    {
        private readonly DataContext _context;

        public DepartamentoRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Departamento Salvar(Departamento obj)
        {
            var retorno = _context.Departamentos.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Departamento obj)
        {
            _context.Entry<Departamento>(obj).State = EntityState.Modified;
        }

        public Departamento Existe(Guid id)
        {
            return _context.Departamentos.Where(s => s.Id == id).FirstOrDefault();
        }
        public Departamento Existe(string nome)
        {
            return _context.Departamentos.Where(s => s.Nome == nome).FirstOrDefault();
        }

        public DetalheDepartamentoResults Detalhes(Guid serieId)
        {
            var retorno = (from s in _context.Departamentos
                           where s.Id == serieId
                           select new DetalheDepartamentoResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).FirstOrDefault();

            return retorno;
        }

        public IList<ListarDepartamentoResults> ListarTodos(Boolean status,int skip,int take)
        {
            var retorno = (from s in _context.Departamentos
                              where s.Status == status
                           select new ListarDepartamentoResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderBy(s => s.Nome).Skip(skip).Take(take).ToList();

            return retorno;
        }
        public IList<ListarDepartamentoResults> ListarTodos()
        {
            var retorno = (from s in _context.Departamentos
                           where s.Status == true
                           select new ListarDepartamentoResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderBy(s => s.Nome).ToList();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
