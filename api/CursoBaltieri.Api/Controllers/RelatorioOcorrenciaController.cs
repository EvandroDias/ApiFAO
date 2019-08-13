using Api.Relatorios;
using Api.Util;
using DinkToPdf;
using DinkToPdf.Contracts;
using Domain.OcorrenciaContent.Commands.Inputs;
using Domain.OcorrenciaContent.Commands.Outputs;
using Domain.OcorrenciaContent.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    //[DisableCors]
    [Route("api/RelatorioOcorrencia")]
    public class RelatorioOcorrenciaController : TransacaoBase
    {
        private readonly IOcorrenciaRepositorio _ocorrenciaRepositorio;
        private IConverter _converter;

        public RelatorioOcorrenciaController(IOcorrenciaRepositorio ocorrenciaRepositorio, IConverter converter, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._ocorrenciaRepositorio = ocorrenciaRepositorio;
            _converter = converter;

        }
        [HttpPost]
       // [Authorize(Policy = "Administrador")]
        [Route("v1/imprimir")]
        public dynamic ListarTodasOcorrencias([FromBody] FiltroOcorrenciaCommands command)
        {
            try
            {

                if (!command.IsValid())
                {
                    return new ComandResult(false, "Por favor corrija os campos abaixo", command.Notifications);
                }
                               

                byte[] file;

                //var t = _ocorrenciaRepositorio.RetornoTotalOcorrencia(command);

                var retorno = _ocorrenciaRepositorio.Filtrar(command);
                

                var obj = TemplateGenerator.ListarOcorrencias(retorno);

                var pdf = RetornoPdf.Retorno(obj, "assets", "styles.css", Orientation.Landscape);
               

                file = _converter.Convert(pdf);
                                
                return File(file, "application/pdf");
            }
            catch(NullReferenceException ex)
            {
                return ex.Message;
            }
           
        }
        [HttpPost]
        // [Authorize(Policy = "Administrador")]
        [Route("v1/imprimir/total")]
        public dynamic RetornoTotalOcorrencia([FromBody] FiltroOcorrenciaCommands command)
        {
            try
            {

                if (!command.IsValid())
                {
                    return new ComandResult(false, "Por favor corrija os campos abaixo", command.Notifications);
                }


                byte[] file;
                             
                var retorno = _ocorrenciaRepositorio.RetornoTotalOcorrencia(command);


                var obj = TemplateGenerator.TotalOcorrencias(retorno);

                var pdf = RetornoPdf.Retorno(obj, "assets", "styles.css", Orientation.Landscape);


                file = _converter.Convert(pdf);

                return File(file, "application/pdf");
            }
            catch (NullReferenceException ex)
            {
                return ex.Message;
            }

        }
        [HttpGet]
        [Route("v1/listar-por-data/{data}")]
        public IActionResult ListarTodasOcorrencias(DateTime data)
        {
            var ocorrencias = _ocorrenciaRepositorio.ListarPorData(data);

            var obj = TemplateGenerator.ListarOcorrencias(ocorrencias);


            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = ConfiguracaoPdf._GlobalSettings(Orientation.Portrait),
                Objects = { ConfiguracaoPdf._ObjectSettings(obj, "assets", "style.css") }
            };

            //_converter.Convert(pdf); IF WE USE Out PROPERTY IN THE GlobalSettings CLASS, THIS IS ENOUGH FOR CONVERSION

            var file = _converter.Convert(pdf);

            //return Ok("Successfully created PDF document.");
            //return File(file, "application/pdf", "EmployeeReport.pdf"); USE THIS RETURN STATEMENT TO DOWNLOAD GENERATED PDF DOCUMENT
            return File(file, "application/pdf");

        }
        [HttpGet]
        [Route("v1/listar-por-periodo/{inicio}/{final}")]
        public IActionResult ListarTodasOcorrencias(DateTime inicio,DateTime final)
        {
            var ocorrencias = _ocorrenciaRepositorio.ListarPorPerido(inicio,final);

            var obj = TemplateGenerator.ListarOcorrencias(ocorrencias);


            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = ConfiguracaoPdf._GlobalSettings(Orientation.Portrait),
                Objects = { ConfiguracaoPdf._ObjectSettings(obj, "assets", "style.css") }
            };

            //_converter.Convert(pdf); IF WE USE Out PROPERTY IN THE GlobalSettings CLASS, THIS IS ENOUGH FOR CONVERSION

            var file = _converter.Convert(pdf);

            //return Ok("Successfully created PDF document.");
            //return File(file, "application/pdf", "EmployeeReport.pdf"); USE THIS RETURN STATEMENT TO DOWNLOAD GENERATED PDF DOCUMENT
            return File(file, "application/pdf");

        }
    }
}