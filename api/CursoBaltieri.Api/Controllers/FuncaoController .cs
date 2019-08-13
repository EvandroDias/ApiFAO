using Api.Util;
using Domain.FuncaoContent.Commands.Inputs;
using Domain.FuncaoContent.Commands.Outputs;
using Domain.FuncaoContent.Handlers;
using Domain.FuncaoContent.Repositories;
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
    [Route("api/Funcao")]
    public class FuncaoController : TransacaoBase
    {
        private readonly IFuncaoRepositorio _funcaoRepositorio;
        private readonly FuncaoHandler _funcaoHandler;

        public FuncaoController(IFuncaoRepositorio funcaoRepositorio, FuncaoHandler funcaoHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._funcaoRepositorio = funcaoRepositorio;
            this._funcaoHandler = funcaoHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarFuncaoCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_funcaoHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarFuncaoCommands command)
        {
            try
            {                
                var result = (ComandResult)_funcaoHandler.Handle(command);

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
        public IList<ListarFuncaoResults> ListarTodos(Boolean status,int skip,int take)
        {
            try
            {
                var result = _funcaoRepositorio.ListarTodos(status,skip,take);

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
        public IList<ListarFuncaoResults> Listar(Boolean status)
        {
            try
            {
                var result = _funcaoRepositorio.Listar(status);

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
                var result = _funcaoHandler.Handle(command);

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
        public DetalheFuncaoResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _funcaoRepositorio.Detalhes(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}