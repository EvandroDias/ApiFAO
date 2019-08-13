using Domain.UserContent.Entities;
using Domain.UserContent.Queries;
using Domain.UserContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using Shared.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DataContext _context;

        public UsuarioRepositorio(DataContext context)
        {
            this._context = context;
        }

        public bool ExisteCpf(string login)
        {
            var existe = _context.Usuarios.Where(u => u.Login == login && u.Status == true).FirstOrDefault();

            if (existe != null)
                return true;

            return false;
            
        }

        public bool ExisteEmail(string email)
        {
            var existe = _context.Usuarios.Where(u => u.Email == email && u.Status == true).FirstOrDefault();

            if (existe != null)
                return true;

            return false;

        }

       

        public Usuario Existe(Guid usuarioId)
        {
            return _context.Usuarios.Where(u => u.Id == usuarioId && u.Status == true).FirstOrDefault();
        }

        public Usuario ExisteAtivoInativo(Guid usuarioId)
        {
            return _context.Usuarios.Where(u => u.Id == usuarioId).FirstOrDefault();
        }

        public bool Excluir(Guid id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                this.Alterar(usuario);
                return true;
            }

            return false;
        }



        public ListarUsuarioQueryResultado BuscarPorId(Guid id)
        {
            var usuario = (from u in _context.Usuarios.Where(u => u.Id == id )
                           select new ListarUsuarioQueryResultado()
                           {

                               Id = u.Id,
                               Nome = u.Nome + " " + u.SobreNome,
                               SobreNome = u.SobreNome,
                               Login = u.Login,
                              // Celular = u.Celular,
                               Status = u.Status


                           }).FirstOrDefault();

            return usuario;
        }
        //public IList<ListarUsuarioQueryResultado> ListarPorFiltro(string filtro)
        //{
        //    var retorno = (from u in _context.Usuarios
        //                   join p in _context.DadoPessoals on u.Id equals p.UsuarioId
        //                   where u.Nome.Contains(filtro) || u.SobreNome.Contains(filtro) || u.Email.Contains(filtro) || p.Celular.Contains(filtro)
        //                   select new ListarUsuarioQueryResultado()
        //                   {
        //                       Id = u.Id,
        //                       Nome = u.Nome + " " + u.SobreNome,
        //                       SobreNome = u.SobreNome,
        //                       Email = u.Email,
        //                       Celular = p.Celular,
        //                       Status = u.Status
        //                   }
        //                   ).ToList();

        //    return retorno;
        //}

        public IList<ListarUsuarioQueryResultado> ListarTodos()
        {
            var retorno = (from u in _context.Usuarios
                           select new ListarUsuarioQueryResultado()
                           {
                               Id= u.Id,
                               Nome = u.Nome,
                               SobreNome = u.SobreNome,
                               Login = u.Login,
                               Status = u.Status

                           }
                            ).OrderByDescending(u => u.Nome).ToList();

            return retorno;
        }

        public IEnumerable<ListarUsuarioQueryResultado> ListarTodos(int skip, int take)
        {
            var retorno = (from u in _context.Usuarios
                           select new ListarUsuarioQueryResultado()
                           {

                               Id = u.Id,
                               Nome = u.Nome + " " + u.SobreNome,
                               SobreNome = u.SobreNome,
                               Login = u.Login,
                               //Celular = p.Celular,
                               Status = u.Status

                           }
                           ).OrderByDescending(u => u.DataCadastro).Skip(skip).Take(take).ToList();

            return retorno;
        }

        public Usuario Salvar(Usuario user)
        {
            var retorno = _context.Usuarios.Add(user);
            return retorno.Entity;
           
        }
        public void Alterar(Usuario user)
        {
            _context.Entry<Usuario>(user).State = EntityState.Modified;
        }

        public Usuario Autenticar(string login,string senha)
        {
            var usuario = _context.Usuarios.Where(u => u.Login == login && u.Status == true).FirstOrDefault();

            if (usuario == null)
                return null;

            string passDecyipt = Crypto.DecryptStringAES(usuario.Senha, usuario.ChaveSenha);

            if (passDecyipt == senha)
                return usuario;

            else return null;
        }

        public Usuario BuscarPorEmail(string login)
        {
            return _context.Usuarios.Where(u => u.Login == login && u.Status == true).FirstOrDefault();
        }

        public void TrocarSenha(Usuario obj)
        {
            this.Alterar(obj);
        }

        public Usuario VerificarCodigoRecuperarSenha(string codigo)
        {
            return _context.Usuarios.Where(u => u.RecuperarSenha == codigo).FirstOrDefault();
        }

        public Usuario BuscarPorTipoUsaurio(string tipo)
        {
            return _context.Usuarios.Where(u => u.TipoUsuario == tipo && u.Status == true).FirstOrDefault();
        }

        public int TotalUsuario(string tipo,int dias=0)
        {
            if(dias > 0)
            {
                var data = DateTime.Now;
                var t = data.AddDays(dias);
                return _context.Usuarios.Where(c => c.TipoUsuario == tipo && c.Status == true && c.DataCadastro >= t && c.DataCadastro <= data).Count();
            }
            else
            {
                return _context.Usuarios.Where(c => c.TipoUsuario == tipo && c.Status == true).Count();
            }
          
           
        }

     
        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
