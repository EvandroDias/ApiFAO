using Api.Util;
using Domain.FuncionarioContent.Commands.Inputs;
using Domain.TipoOcorrenciaContent.Commands.Inputs;
using Domain.TipoOcorrenciaContent.Commands.Outputs;
using Domain.TipoOcorrenciaContent.Handlers;
using Domain.TipoOcorrenciaContent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/TipoOcorrencia")]
    public class TipoOcorrenciaController : TransacaoBase
    {
        private readonly ITipoOcorrenciaRepositorio _tipoOcorrenciaRepositorio;
        private readonly TipoOcorrenciaHandler _tipoOcorrenciaHandler;

        public TipoOcorrenciaController(ITipoOcorrenciaRepositorio tipoOcorrenciaRepositorio, TipoOcorrenciaHandler tipoOcorrenciaHandler,IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._tipoOcorrenciaRepositorio = tipoOcorrenciaRepositorio;
            this._tipoOcorrenciaHandler = tipoOcorrenciaHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarTipoOcorrenciaCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            //command.SetarUsuarioId(user);
            var result = (ComandResult)_tipoOcorrenciaHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarTipoOcorrenciaCommands command)
        {
            try
            {
                var result = (ComandResult)_tipoOcorrenciaHandler.Handle(command);

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
        [Route("v1/listar")]
        public IList<ListarTipoOcorrenciaResults> Listar()
        {
            try
            {
                var result = _tipoOcorrenciaRepositorio.ListarTodos();

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
        public IList<ListarTipoOcorrenciaResults> Listar(Boolean status, int skip, int take)
        {
            try
            {
                var result = _tipoOcorrenciaRepositorio.ListarTodos(status, skip, take);

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
                var result = _tipoOcorrenciaHandler.Handle(command);

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
        [Route("v1/listar-cmb")]
        public IList<ListarTipoOcorrenicaCmbResults> ListarCmb()
        {
            try
            {
                var result = _tipoOcorrenciaRepositorio.ListarCmb();

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
        [Route("v1/detalhe/{id}")]
        public DetalheTipoOcorrenciaResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _tipoOcorrenciaRepositorio.Detalhes(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}