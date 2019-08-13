using Domain.DadoPessoalContent.Commands.Outputs;
using Domain.DadoPessoalContent.Entities;
using Domain.DadoPessoalContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infra.Repositories
{
    public class DadoPessoalRepositorio : IDadoPessoalRepositorio
    {
        private readonly DataContext _context;

        public DadoPessoalRepositorio(DataContext context)
        {
            this._context = context;
        }

        public void Alterar(DadoPessoal dadoPessoal)
        {
            _context.Entry<DadoPessoal>(dadoPessoal).State = EntityState.Modified;
        }

        public DadoPessoal BuscarPorId(Guid id)
        {
            return _context.DadoPessoals.Where(u => u.Id == id).FirstOrDefault();
        }

        //public DadoPessoal BuscarPorUsuarioId(Guid usuarioId)
        //{
         
        //    return _context.DadoPessoals.Where(u => u.UsuarioId == usuarioId).FirstOrDefault();
        //}
        //public DadoPessoalCommandResult ListarPorUsuarioId(Guid usuarioId)
        //{
        //    var retorno = (from d in _context.DadoPessoals
        //                   where d.UsuarioId == usuarioId
        //                   select new DadoPessoalCommandResult()
        //                   {
        //                       Rua = d.Rua,
        //                       Numero = d.Numero,
        //                       Bairro = d.Bairro,
        //                       Cep = d.Cep,
        //                       Cidade = d.Cidade,
        //                       Uf = d.Uf,
        //                       Celular = d.Celular,
        //                       TelefoneFixo = d.TelefoneFixo,
        //                       WattsApp = d.WattsApp,
        //                       Complemento = d.Complemento,
        //                       Latitude = d.Latitude,
        //                       Longitude = d.Longitude

        //                   }).FirstOrDefault();
        //    return retorno;
        //    //return _context.DadoPessoals.Where(u => u.UsuarioId == usuarioId).FirstOrDefault();
        //}


        public bool Existe(Guid Id)
        {
            var existe = _context.DadoPessoals.Where(u => u.Id == Id).FirstOrDefault();

            if (existe != null)
                return true;

            return false;
        }

        public DadoPessoal Salvar(DadoPessoal dadoPessoal)
        {
            var retorno = _context.DadoPessoals.Add(dadoPessoal);

            return retorno.Entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
