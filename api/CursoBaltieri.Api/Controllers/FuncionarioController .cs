using Api.Util;
using Domain.FuncionarioContent.Commands.Inputs;
using Domain.FuncionarioContent.Commands.Outputs;
using Domain.FuncionarioContent.Handlers;
using Domain.FuncionarioContent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Funcionario")]
    public class FuncionarioController : TransacaoBase
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly FuncionarioHandler _funcionarioHandler;

        public FuncionarioController(IFuncionarioRepositorio funcionarioRepositorio, FuncionarioHandler funcionarioHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._funcionarioRepositorio = funcionarioRepositorio;
            this._funcionarioHandler = funcionarioHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarFuncionarioCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("bfbed547-0e0c-4c3c-86cc-4e92cd0fd1c8");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_funcionarioHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
    
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarFuncionarioCommands command)
        {
            try
            {                
                var result = (ComandResult)_funcionarioHandler.Handle(command);

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
        public DetalheFuncionarioResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _funcionarioRepositorio.Detalhes(id);

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
        public IList<ListarFuncionarioResults> ListarTodosFuncionario(Boolean status,int skip,int take)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _funcionarioRepositorio.ListarTodos(status,skip,take);

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
        public IList<ListarFuncionarioResults> Pesquisar(string nome)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _funcionarioRepositorio.Pesquisar(nome);

                return result;
            }
            catch (Exception ex)
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
                var result = _funcionarioHandler.Handle(command);

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
        public IList<ListarFuncionarioCmbResults> ListarCmb()
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _funcionarioRepositorio.ListarCmb();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/listar-funcao/{nome}")]
        public IList<ListarFuncionarioCmbResults> ListarFuncao(string nome)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _funcionarioRepositorio.ListarCordenador(nome);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}