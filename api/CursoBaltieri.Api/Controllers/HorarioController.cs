using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Util;
using Domain.HorarioRotinaContent.Commands.Inputs;
using Domain.HorarioRotinaContent.Commands.Outputs;
using Domain.HorarioRotinaContent.Handlers;
using Domain.HorarioRotinaContent.Repositories;
using Domain.RotinaContent.Commands.Outputs;
using Domain.RotinaContent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/HorarioRotina")]
    public class HorarioRotinaController : TransacaoBase
    {
        private readonly IHorarioRotinaRepositorio _horarioRotinaRepositorio;
        private readonly IRotinaRepositorio _rotinaRepositorio;
        private readonly HorarioRotinaHandler _horarioRotinaHandler;

        public HorarioRotinaController(IRotinaRepositorio rotinaRepositorio,IHorarioRotinaRepositorio horarioRotinaRepositorio, HorarioRotinaHandler horarioRotinaHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._horarioRotinaRepositorio = horarioRotinaRepositorio;
            this._horarioRotinaHandler = horarioRotinaHandler;
            this._rotinaRepositorio = rotinaRepositorio;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Route("v1/salvar")]
        public IComandResult Post([FromBody]SalvarHorarioRotinaCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            //command.SetarUsuarioId(user);
            var result = (ComandResult)_horarioRotinaHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarHorarioRotinaCommands command)
        {
            try
            {
                var result = (ComandResult)_horarioRotinaHandler.Handle(command);

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
        [Route("v1/listar/{rotinaId}/{diaSemanaId}")]
        public IList<ListarHorarioRotinaResults> Listar(Guid rotinaId,Guid diaSemanaId)
        {
            return _horarioRotinaRepositorio.ListarMeusHorarios(rotinaId,diaSemanaId);
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/listar-todos/{rotinaId}")]
        public dynamic ListarTodos(Guid rotinaId)
        {
            var retorno = _rotinaRepositorio.Imprimir(rotinaId);

            var t = retorno.ListarHorarioRotinaResults.GroupBy(s => s.Aula).Select(c => c.ToList().OrderBy(a=>a.Order));

                       

            

            if (retorno != null)
            {
                return t;
            }

            return null;
            
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/detalhe-admin/{id}")]
        public DetalheHorarioRotinaResults DetalheAdmin(Guid id)
        {
            try
            {
                
                var result = _horarioRotinaRepositorio.Detalhes(id);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/detalhe/{id}")]
        public DetalheHorarioRotinaResults Detalhe(Guid id)
        {
            try
            {
                var user = Guid.Parse(this.User.Identity.Name);
                var result = _horarioRotinaRepositorio.Detalhes(id,user);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}