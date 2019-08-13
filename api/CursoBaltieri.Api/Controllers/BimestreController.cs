using Api.Util;
using Domain.BimestreContent.Commands.Inputs;
using Domain.BimestreContent.Commands.Outputs;
using Domain.BimestreContent.Handlers;
using Domain.BimestreContent.Repositories;
using Domain.FuncionarioContent.Commands.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Bimestre")]
    public class BimestreController : TransacaoBase
    {
        private readonly IBimestreRepositorio _bimestreRepositorio;
        private readonly BimestreHandler _bimestreHandler;

        public BimestreController(IBimestreRepositorio bimestreRepositorio, BimestreHandler bimestreHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._bimestreRepositorio = bimestreRepositorio;
            this._bimestreHandler = bimestreHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarBimestreCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_bimestreHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarBimestreCommands command)
        {
            try
            {                
                var result = (ComandResult)_bimestreHandler.Handle(command);

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
        [Authorize(Policy = "Administrador")]
        [Route("v1/listar-todos/{status}/{skip}/{take}")]
        public IList<ListarBimestreResults> ListarTodos(Boolean status,int skip,int take)
        {
            try
            {
                var result = _bimestreRepositorio.ListarTodos(status,skip,take);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/listar/{status}")]
        public IList<ListarBimestreResults> Listar(Boolean status)
        {
            try
            {
                var result = _bimestreRepositorio.Listar(status);

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
        public IComandResult AtivarDesativar([FromBody]AtivarDesativarCommands command)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _bimestreHandler.Handle(command);

                this.Commit(result.Success);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpGet]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/detalhe/{id}")]
        public DetalheBimestreResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _bimestreRepositorio.Detalhes(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}