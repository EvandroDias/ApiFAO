using Domain.ProvidenciaContent.Entities;
using Domain.ProvidenciaContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infra.Repositories
{
    public class ProvidenciaRepositorio : IProvidenciaRepositorio
    {
        private readonly DataContext _context;

        public ProvidenciaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Providencia Salvar(Providencia obj)
        {
            var retorno = _context.Providencias.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Providencia obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public Providencia Existe(Guid id)
        {
            return _context.Providencias.Where(s => s.Id == id).FirstOrDefault();
        }

      
     

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
