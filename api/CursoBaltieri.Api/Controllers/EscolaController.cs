using Api.Util;
using Domain.EscolaContent.Commands.Inputs;
using Domain.EscolaContent.Commands.Outputs;
using Domain.EscolaContent.Handlers;
using Domain.EscolaContent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Escola")]
    public class EscolaController : TransacaoBase
    {
        private readonly IEscolaRepositorio _escolaRepositorio;
        private readonly EscolaHandler _escolaHandler;

        public EscolaController(IEscolaRepositorio escolaRepositorio, EscolaHandler escolaHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._escolaRepositorio = escolaRepositorio;
            this._escolaHandler = escolaHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarEscolaCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_escolaHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/adicionar-departamento")]
        public IComandResult AdicionarDepartamento([FromBody]AdicionarDepartamentoCommands command)
        {
                  
            var result = (ComandResult)_escolaHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarEscolaCommands command)
        {
            try
            {                
                var result = (ComandResult)_escolaHandler.Handle(command);

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
        //[Authorize(Policy = "Administrador")]
        [Route("v1/listar-todos")]
        public IList<ListarEscolaResults> ListarTodos()
        {
            try
            {
                var result = _escolaRepositorio.ListarTodos();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}