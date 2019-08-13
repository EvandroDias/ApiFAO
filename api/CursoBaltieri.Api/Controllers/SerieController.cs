using Api.Util;
using Domain.FuncionarioContent.Commands.Inputs;
using Domain.SerieContent.Commands.Inputs;
using Domain.SerieContent.Commands.Outputs;
using Domain.SerieContent.Handlers;
using Domain.SerieContent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Serie")]
    public class SerieController : TransacaoBase
    {
        private readonly ISerieRepositorio _serieRepositorio;
        private readonly SerieHandler _serieHandler;

        public SerieController(ISerieRepositorio serieRepositorio, SerieHandler serieHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._serieRepositorio = serieRepositorio;
            this._serieHandler = serieHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarSerieCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_serieHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarSerieCommands command)
        {
            try
            {                
                var result = (ComandResult)_serieHandler.Handle(command);

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
        public IList<ListarSerieResults> ListarTodos(Boolean status, int skip, int take)
        {
            try
            {
                var result = _serieRepositorio.ListarTodos(status, skip, take);

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
                var result = _serieHandler.Handle(command);

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
        [Route("v1/listar/{status}")]
        public IList<ListarSerieResults> Listar(Boolean status)
        {
            try
            {
                var result = _serieRepositorio.Listar(status);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/listar-cmb")]
        public IList<ListarSerieCmbResults> ListarCmb()
        {
            try
            {
                var result = _serieRepositorio.ListarCmb();

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
        public DetalheSerieResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _serieRepositorio.Detalhes(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}