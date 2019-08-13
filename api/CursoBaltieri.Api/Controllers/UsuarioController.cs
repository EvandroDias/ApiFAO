using Api.Util;
using Domain.UserContent.Commands.Inputs;
using Domain.UserContent.Handlers;
using Domain.UserContent.Queries;
using Domain.UserContent.Repositories;
using Infra.Services;
using Shared.Comands;
using Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/usuario/")]
    public class UsuarioController : TransacaoBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly UsuarioHandler _usuarioHandler;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, UsuarioHandler usuarioHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._usuarioRepositorio = usuarioRepositorio;
            this._usuarioHandler = usuarioHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/registrar")]
        public IComandResult Post([FromBody]RegistroUsuarioComand command)
        {
            var result = (ComandResult)_usuarioHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarUsuarioComand command)
        {
            try
            {

                var user = Guid.Parse(this.User.Identity.Name);
                command.SetarUsuarioId(user);
                var result = (ComandResult)_usuarioHandler.Handle(command);

                this.Commit(result.Success);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/ativar-desativar")]
        public IComandResult Ativar([FromBody]AtivarDesativarUsuarioCommand command)
        {
            try
            {

                //var user = Guid.Parse(this.User.Identity.Name);
               
                var result = (ComandResult)_usuarioHandler.Handle(command);

                this.Commit(result.Success);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/listar-dado-acesso")]
        public ListarUsuarioQueryResultado Get()
        {
            try
            {
                var user = Guid.Parse(this.User.Identity.Name);
                var result = _usuarioRepositorio.BuscarPorId(user);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
          
        }

        //[HttpGet]
        //[Authorize("Bearer")]
        //[Route("v1/listar-usuario/{filtro}")]
        //public IList<ListarUsuarioQueryResultado> ListarUsuario(string filtro)
        //{
        //    try
        //    {
        //        //var user = Guid.Parse(this.User.Identity.Name);
        //        var result = _usuarioRepositorio.ListarPorFiltro(filtro);

        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //}

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/detalhe/{id}")]
        public ListarUsuarioQueryResultado Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _usuarioRepositorio.BuscarPorId(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }

        [HttpGet]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/listar-todos-usuario")]
        public IList<ListarUsuarioQueryResultado> ListarTodosUsuario()
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _usuarioRepositorio.ListarTodos();

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }

        [HttpPost]
        [Route("v1/usuario/email-esqueceu-senha")]
        public IComandResult EmailEsqueceuSenha([FromBody]EmailEsqueceuSenhaComand command)
        {
            var result = (ComandResult)_usuarioHandler.Handle(command);
           
            this.Commit(result.Success);
                       
            
            return result;
        }

        [HttpPost]
        [Route("v1/usuario/esqueceu-senha")]
        public IComandResult EsqueceuSenha([FromBody]EsqueceuSenhaComand command)
        {
            var result = (ComandResult)_usuarioHandler.Handle(command);

            this.Commit(result.Success);


            return result;
        }

        [HttpPost]
        [Route("v1/usuario/trocar-senha")]
        public IComandResult TrocarSenha([FromBody]TrocarSenhaUsuarioComand command)
        {
            var user = Guid.Parse(this.User.Identity.Name);
            command.SetarUsuarioId(user);

            var result = (ComandResult)_usuarioHandler.Handle(command);

            this.Commit(result.Success);


            return result;
        }

        [HttpGet]
        [Route("v1/usuario/total/{tipo}/{dias}")]
        public int Total(string tipo,int dias)
        {
            var result = _usuarioRepositorio.TotalUsuario(tipo,dias);
            return result;
        }
    }
}