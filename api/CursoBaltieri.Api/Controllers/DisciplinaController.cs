using Api.Util;
using Domain.DisciplinaContent.Commands.Inputs;
using Domain.DisciplinaContent.Commands.Outputs;
using Domain.DisciplinaContent.Handlers;
using Domain.FuncionarioContent.Commands.Inputs;
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
    [Route("api/Disciplina")]
    public class DisciplinaController : TransacaoBase
    {
        private readonly IDisciplinaRepositorio _disciplinaRepositorio;
        private readonly DisciplinaHandler _disciplinaHandler;

        public DisciplinaController(IDisciplinaRepositorio disciplinaRepositorio, DisciplinaHandler disciplinaHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._disciplinaRepositorio = disciplinaRepositorio;
            this._disciplinaHandler = disciplinaHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarDisciplinaCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_disciplinaHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarDisciplinaCommands command)
        {
            try
            {                
                var result = (ComandResult)_disciplinaHandler.Handle(command);

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
        public IList<ListarDisciplinaResults> Listar()
        {
            try
            {
                var result = _disciplinaRepositorio.ListarTodos();

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
        public IList<ListarDisciplinaResults> Listar(Boolean status, int skip, int take)
        {
            try
            {
                var result = _disciplinaRepositorio.ListarTodos(status, skip, take);

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
                var result = _disciplinaHandler.Handle(command);

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
        public DetalheDisciplinaResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _disciplinaRepositorio.Detalhes(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}