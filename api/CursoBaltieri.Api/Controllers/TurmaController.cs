using Api.Util;
using Domain.AlunoTurmaContent.Commands.Inputs;
using Domain.AlunoTurmaContent.Repositories;
using Domain.AnoContent.Repositories;
using Domain.ItemAlunoTurmaContent.Handlers;
using Domain.TurmaContent.Commands.Inputs;
using Domain.TurmaContent.Commands.Outputs;
using Domain.TurmaContent.Handlers;
using Domain.TurmaContent.Repositories;
using Infra.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Turma")]
    public class TurmaController : TransacaoBase
    {
        private readonly ITurmaRepositorio _turmaRepositorio;
        private readonly ItemAlunoTurmaHandler _itemTurmaHandler;
        private readonly IItemAlunoTurmaRepositorio _itemTurmaRepositorio;
        private readonly IAnoRepositorio _anoRepositorio;
        private readonly TurmaHandler _turmaHandler;

        public TurmaController(IItemAlunoTurmaRepositorio itemTurmaRepositorio, ItemAlunoTurmaHandler itemTurmaHandler,IAnoRepositorio anoRepositorio,ITurmaRepositorio turmaRepositorio, TurmaHandler turmaHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._turmaRepositorio = turmaRepositorio;
            this._turmaHandler = turmaHandler;
            this._anoRepositorio = anoRepositorio;
            this._itemTurmaHandler = itemTurmaHandler;
            this._itemTurmaRepositorio = itemTurmaRepositorio;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/detalhe/{id}")]
        public DetalheTurmaResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _turmaRepositorio.Detalhes(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/detalhe-aluno/{alunoId}/{turmaId}")]
        public DetalheAlunoTurmaResults DetalheAluno(Guid alunoId, Guid turmaId)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _itemTurmaRepositorio.Detalhe(alunoId, turmaId);

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
        public IList<ListarTurmaResults> ListarTodos(Boolean status, int skip, int take)
        {
            try
            {
                var a = DateTime.Now.Year.ToString();
                var ano = _anoRepositorio.Existe(a);

                if(ano != null)
                {
                    var result = _turmaRepositorio.ListarTodos(status, skip, take, ano.Id);

                    return result;
                }

                return null;
                
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/listar-por-turmaid/{turmaId}")]
        public IList<ListarTurmaResults> ListarPorTurmaId(Guid turmaId)
        {
            try
            {
                var a = DateTime.Now.Year.ToString();
                var ano = _anoRepositorio.Existe(a);

                if (ano != null)
                {
                    var result = _turmaRepositorio.ListarPorId(turmaId,ano.Id);

                    return result;
                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar-aluno")]
        public IComandResult SalvarAluno([FromBody]SalvarItemAlunoTurmaCommands command)
        {

           // var user = this.User.Identity.Name;
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            //command.SetarUsuarioId(user);
            var result = (ComandResult)_itemTurmaHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar-aluno")]
        public IComandResult AlterarAluno([FromBody]AlterarAlunoTurmaCommands command)
        {
                        
            var result = (ComandResult)_itemTurmaHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarTurmaCommands command)
        {

            var user = this.User.Identity.Name;
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_turmaHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
    }
}