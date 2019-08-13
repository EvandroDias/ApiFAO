using Api.Util;
using Domain.FuncionarioContent.Commands.Inputs;
using Domain.OcorrenciaContent.Commands.Inputs;
using Domain.OcorrenciaContent.Commands.Outputs;
using Domain.OcorrenciaContent.Handlers;
using Domain.OcorrenciaContent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Ocorrencia")]
    public class OcorrenciaController : TransacaoBase
    {
        private readonly IOcorrenciaRepositorio _ocorrenciaRepositorio;
        private readonly OcorrenciaHandler _ocorrenciaHandler;

        public OcorrenciaController(IOcorrenciaRepositorio ocorrenciaRepositorio, OcorrenciaHandler ocorrenciaHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._ocorrenciaRepositorio = ocorrenciaRepositorio;
            this._ocorrenciaHandler = ocorrenciaHandler;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/detalhe/{id}")]
        public DetalheOcorrenciaResults Detalhe(Guid id)
        {
            try
            {
                //var user = Guid.Parse(this.User.Identity.Name);
                var result = _ocorrenciaRepositorio.Detalhes(id);

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
        public IComandResult Post([FromBody]SalvarOcorrenciaCommands command)
        {

            var user = Guid.Parse(this.User.Identity.Name);
            //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
            command.SetarUsuarioId(user);
            var result = (ComandResult)_ocorrenciaHandler.Handle(command);
            this.Commit(result.Success);
            return result;
        }
        [HttpPost]
        [Authorize("Bearer")]
        [Route("v1/alterar")]
        public IComandResult Alterar([FromBody]AlterarOcorrenciaCommands command)
        {
            try
            {
                var result = (ComandResult)_ocorrenciaHandler.Handle(command);

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
        public IList<ListarOcorrenciaResults> ListarTodos(int skip,int take)
        {                                     
            return _ocorrenciaRepositorio.ListarTodos(skip,take);
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/listar-minhas-ocorrencias/{skip}/{take}")]
        public IList<ListarOcorrenciaResults> ListarMinhasOcorrencias(int skip, int take)
        {
            var user = Guid.Parse(this.User.Identity.Name);
            return _ocorrenciaRepositorio.ListarMinhasOcorrencias(skip, take,user);
        }
        [HttpGet]
        [Authorize("Bearer")]
        [Route("v1/listar-por-data/{data}")]
        public IList<ListarOcorrenciaResults> ListarTodasOcorrencias(DateTime data)
        {
            var ocorrencias = _ocorrenciaRepositorio.ListarPorData(data);

            return ocorrencias;

        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/filtrar")]
        public List<ListarOcorrenciaResults> Filtro([FromBody]FiltroOcorrenciaCommands command)
        {
            //var ocorrencias = _ocorrenciaHandler.Handle(command);
            var ocorrencia = new List<ListarOcorrenciaResults>();

            switch (command.TipoFiltro)
            {
                case "Série":
                    return ocorrencia = _ocorrenciaRepositorio.FiltrarPorSerie(command);
                   
                case "Aluno":
                    return ocorrencia = _ocorrenciaRepositorio.FiltrarPorAluno(command);

                case "Data":
                    return ocorrencia = _ocorrenciaRepositorio.FiltrarTodoPorData(command);

                default:
                   return  null;
            }

            //return ocorrencias;

        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/painel-grafico")]
        public PainelGraficoOcorrenciaResults PainelGrafico([FromBody]FiltroOcorrenciaCommands command)
        {
            //var ocorrencias = _ocorrenciaHandler.Handle(command);
            var ocorrencia = new PainelGraficoOcorrenciaResults();

         
            return ocorrencia = _ocorrenciaRepositorio.GraficoPainel(command);

            //return ocorrencias;

        }
        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/grafico-por-serie")]
        public PainelGraficoOcorrenciaResults GraficoPorSerie([FromBody]FiltroOcorrenciaCommands command)
        {
            //var ocorrencias = _ocorrenciaHandler.Handle(command);
            var ocorrencia = new PainelGraficoOcorrenciaResults();


            return ocorrencia = _ocorrenciaRepositorio.GraficoPorSerie(command);

            //return ocorrencias;

        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/grafico")]
        public GraficoOcorrenciaResults Grafico([FromBody]FiltroOcorrenciaCommands command)
        {
            
            var ocorrencia = new GraficoOcorrenciaResults();

         
            return ocorrencia = _ocorrenciaRepositorio.GraficoPorData(command);

          
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
                var result = _ocorrenciaHandler.Handle(command);

                this.Commit(result.Success);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}