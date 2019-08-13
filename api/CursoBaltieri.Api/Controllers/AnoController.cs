using Api.Util;
using Domain.AnoContent.Commands.Inputs;
using Domain.AnoContent.Commands.Outputs;
using Domain.AnoContent.Handlers;
using Domain.AnoContent.Repositories;
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
    [Route("api/Ano")]
    public class AnoController : TransacaoBase
    {
        private readonly IAnoRepositorio _anoRepositorio;
        private readonly AnoHandler _anoHandler;

        public AnoController(IAnoRepositorio anoRepositorio, AnoHandler anoHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._anoRepositorio = anoRepositorio;
            this._anoHandler = anoHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarAnoCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_anoHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarAnoCommands command)
        {
            try
            {                
                var result = (ComandResult)_anoHandler.Handle(command);

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
        public IList<ListarAnoResults> ListarTodos(Boolean status,int skip,int take)
        {
            try
            {
                var result = _anoRepositorio.ListarTodos(status,skip,take);

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
        public IList<ListarAnoResults> Listar(Boolean status)
        {
            try
            {
                var result = _anoRepositorio.Listar(status);

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
                var result = _anoHandler.Handle(command);

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
        public DetalheAnoResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _anoRepositorio.Detalhes(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}