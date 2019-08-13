using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Util;
using Domain.FuncionarioContent.Commands.Inputs;
using Domain.RotinaContent.Commands.Inputs;
using Domain.RotinaContent.Commands.Outputs;
using Domain.RotinaContent.Handlers;
using Domain.RotinaContent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Rotina")]
    public class RotinaController : TransacaoBase
    {
        private readonly IRotinaRepositorio _rotinaRepositorio;
        private readonly RotinaHandler _rotinaHandler;

        public RotinaController(IRotinaRepositorio rotinaRepositorio, RotinaHandler rotinaHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._rotinaRepositorio = rotinaRepositorio;
            this._rotinaHandler = rotinaHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarRotinaCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_rotinaHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarRotinaCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_rotinaHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/detalhe/{id}")]
        public DetalheRotinaResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _rotinaRepositorio.Detalhes(id);

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
        [Route("v1/listar-todos/{skip}/{take}")]
        public IList<ListarRotinaResults> ListarTodos(int skip, int take)
        {
            return _rotinaRepositorio.ListarTodos(skip,take);
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/listar-minhas-rotinas/{skip}/{take}")]
        public IList<ListarRotinaResults> ListarMinhasRotinas(int skip,int take)
        {
            var user = Guid.Parse(this.User.Identity.Name);
            return _rotinaRepositorio.ListarMinhasRotinas(skip,take,user);
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Route("v1/ativar-desativar")]
        public IComandResult AtivarDesativar([FromBody]AtivarDesativarCommands command)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _rotinaHandler.Handle(command);

                this.Commit(result.Success);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}