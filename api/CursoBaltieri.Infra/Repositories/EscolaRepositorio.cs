using Domain.EscolaContent.Commands.Outputs;
using Domain.EscolaContent.Entities;
using Domain.EscolaContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class EscolaRepositorio : IEscolaRepositorio
    {
        private readonly DataContext _context;

        public EscolaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Escola Salvar(Escola obj)
        {
            var retorno = _context.Escolas.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Escola obj)
        {
            _context.Entry<Escola>(obj).State = EntityState.Modified;
        }

        public Escola Existe(Guid id)
        {
            return _context.Escolas.Where(s => s.Id == id).FirstOrDefault();
        }

        public Escola Existe(string nome)
        {
            return _context.Escolas.Where(s => s.Nome == nome).FirstOrDefault();
        }

        public DetalheEscolaResults Detalhes(Guid serieId)
        {
            var retorno = (from s in _context.Escolas
                           where s.Id == serieId
                           select new DetalheEscolaResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id
                           }).FirstOrDefault();

            return retorno;
        }

        public IList<ListarEscolaResults> ListarTodos()
        {
            var retorno = (from s in _context.Escolas
                           select new ListarEscolaResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id
                           }).OrderByDescending(s => s.Nome).ToList();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
