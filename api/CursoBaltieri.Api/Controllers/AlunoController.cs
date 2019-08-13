using Api.Util;
using Domain.AlunoContent.Commands.Inputs;
using Domain.AlunoContent.Commands.Outputs;
using Domain.AlunoContent.Handlers;
using Domain.AlunoContent.Repositories;
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
    [Route("api/Aluno")]
    public class AlunoController : TransacaoBase
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly AlunoHandler _alunoHandler;

        public AlunoController(IAlunoRepositorio serieRepositorio, AlunoHandler alunoHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._alunoRepositorio = serieRepositorio;
            this._alunoHandler = alunoHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarAlunoCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_alunoHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarAlunoCommands command)
        {
            try
            {                
                var result = (ComandResult)_alunoHandler.Handle(command);

                this.Commit(result.Success);
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
                var result = _alunoHandler.Handle(command);

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
        public DetalheAlunoResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _alunoRepositorio.Detalhes(id);

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
        [Route("v1/listar-todos-alunos/{status}/{skip}/{take}")]
        public IList<ListarAlunoResults> ListarTodosAlunos(Boolean status,int skip,int take)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _alunoRepositorio.ListarTodos(status,skip,take);

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
        [Route("v1/pesquisar/{nome}")]
        public IList<ListarAlunoResults> Pesquisar(string nome)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _alunoRepositorio.Pesquisar(nome);

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
        public IList<ListarAlunoCmbResults> ListarCmb()
        {
            try
            {
                var result = _alunoRepositorio.ListarCmb();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/listar-cmb/{id}")]
        public IList<ListarAlunoCmbResults> ListarCmb(Guid id)
        {
            try
            {
                var result = _alunoRepositorio.ListarCmb(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/listar-por-turmaId/{turmaId}")]
        public IList<ListarAlunoCmbResults> ListarPorTurmaId(Guid turmaId)
        {
            try
            {
                var result = _alunoRepositorio.ListarPorTurmaId(turmaId);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}