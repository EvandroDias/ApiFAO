using Api.Util;
using Domain.AlunoConselhoContent.Commands.Inputs;
using Domain.AlunoConselhoContent.Commands.Outputs;
using Domain.AlunoConselhoContent.Handlers;
using Domain.AlunoConselhoContent.Repositories;
using Domain.ConselhoContent.Commands.Outputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/AlunoConselho")]
    public class AlunoConselhoController : TransacaoBase
    {
        private readonly IAlunoConselhoRepositorio _alunoConselhoRepositorio;
        private readonly AlunoConselhoHandler _alunoConselhoHandler;

        public AlunoConselhoController(IAlunoConselhoRepositorio alunoConselhoRepositorio, AlunoConselhoHandler alunoConselhoHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._alunoConselhoRepositorio = alunoConselhoRepositorio;
            this._alunoConselhoHandler = alunoConselhoHandler;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/detalhe/{id}")]
        public DetalheAlunoConselhoResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _alunoConselhoRepositorio.Detalhes(id);

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
        public IComandResult Post([FromBody]SalvarAlunoConselhoCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_alunoConselhoHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarAlunoConselhoCommands command)
        {
            try
            {
                var result = (ComandResult)_alunoConselhoHandler.Handle(command);

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
        [Route("v1/listar-todos/{conselhoId}/")]
        public IList<ListarAlunoConselhoResults> ListarTodos(Guid conselhoId)
        {                                     
            return _alunoConselhoRepositorio.ListarTodos(conselhoId);
        }

       
     

       
    }
}