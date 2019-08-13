using Api.Util;
using Domain.DepartamentoContent.Commands.Inputs;
using Domain.DepartamentoContent.Commands.Outputs;
using Domain.DepartamentoContent.Handlers;
using Domain.DepartamentoContent.Repositories;
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
    [Route("api/Departamento")]
    public class DepartamentoController : TransacaoBase
    {
        private readonly IDepartamentoRepositorio _departamentoRepositorio;
        private readonly DepartamentoHandler _departamentoHandler;

        public DepartamentoController(IDepartamentoRepositorio departamentoRepositorio, DepartamentoHandler departamentoHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._departamentoRepositorio = departamentoRepositorio;
            this._departamentoHandler = departamentoHandler;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarDepartamentoCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_departamentoHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarDepartamentoCommands command)
        {
            try
            {                
                var result = (ComandResult)_departamentoHandler.Handle(command);

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
        public IList<ListarDepartamentoResults> Listar(Boolean status,int skip,int take)
        {
            try
            {
                var result = _departamentoRepositorio.ListarTodos(status,skip,take);

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
        [Route("v1/listar-todos")]
        public IList<ListarDepartamentoResults> ListarTodos()
        {
            try
            {
                var result = _departamentoRepositorio.ListarTodos();

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
                var result = _departamentoHandler.Handle(command);

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
        public DetalheDepartamentoResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _departamentoRepositorio.Detalhes(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}