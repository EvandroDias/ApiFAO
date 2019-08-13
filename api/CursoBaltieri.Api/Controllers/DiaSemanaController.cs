using Api.Util;
using Domain.DiaSemanaContent.Commands.Outputs;
using Domain.DiaSemanaContent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/DiaSemana")]
    public class DiaSemanaController : TransacaoBase
    {
        private readonly IDiaSemanaRepositorio _diaSemanaRepositorio;
        

        public DiaSemanaController(IDiaSemanaRepositorio diaSemanaRepositorio, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._diaSemanaRepositorio = diaSemanaRepositorio;
           
        }

  
        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/listar")]
        public IList<ListarDiaSemanaResults> Listar()
        {
            return _diaSemanaRepositorio.ListarTodos();
        }
    }
}