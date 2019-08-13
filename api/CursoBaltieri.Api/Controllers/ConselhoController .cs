using Api.Util;
using Domain.ConselhoContent.Commands.Inputs;
using Domain.ConselhoContent.Commands.Outputs;
using Domain.ConselhoContent.Handlers;
using Domain.ConselhoContent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Conselho")]
    public class ConselhoController : TransacaoBase
    {
        private readonly IConselhoRepositorio _conselhoRepositorio;
        private readonly ConselhoHandler _conselhoHandler;

        public ConselhoController(IConselhoRepositorio conselhoRepositorio, ConselhoHandler conselhoHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._conselhoRepositorio = conselhoRepositorio;
            this._conselhoHandler = conselhoHandler;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/detalhe/{id}")]
        public DetalheConselhoResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _conselhoRepositorio.Detalhes(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }

        [HttpPost]
        [Authorize("Bearer")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarConselhoCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_conselhoHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarConselhoCommands command)
        {
            try
            {
                var result = (ComandResult)_conselhoHandler.Handle(command);

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
        [Route("v1/listar-todos/{skip}/{take}")]
        public IList<ListarConselhoResults> ListarTodos(int skip,int take)
        {                                     
            return _conselhoRepositorio.ListarTodos(skip,take);
        }

       
        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/listar-por-data/{data}")]
        public IList<ListarConselhoResults> ListarTodasConselhos(DateTime data)
        {
            var conselhos = _conselhoRepositorio.ListarPorData(data);

            return conselhos;

        }

       
    }
}