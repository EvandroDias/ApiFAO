using Api.Relatorios;
using Api.Util;
using DinkToPdf;
using DinkToPdf.Contracts;
using Domain.ConselhoContent.Commands.Inputs;
using Domain.ConselhoContent.Commands.Outputs;
using Domain.ConselhoContent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/RelatorioConselho")]
    public class RelatorioConselhoController : TransacaoBase
    {
        private readonly IConselhoRepositorio _conselhoRepositorio;
        private IConverter _converter;

        public RelatorioConselhoController( IConverter converter, IConselhoRepositorio conselhoRepositorio, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._conselhoRepositorio = conselhoRepositorio;
            _converter = converter;

        }
        [HttpPost]
       // [Authorize("Bearer")]
       // [Authorize(Policy = "Administrador")]
        [Route("v1/imprimir")]
        public IActionResult Imprimir([FromBody]FiltroConselhoCommands command)
        {
            //var conselhos = _conselhoHandler.Handle(command);
            var conselho = new List<ListarConselhoResults>();

            //switch (command.TipoFiltro)
            //{
            //    case "Série":
                    conselho = _conselhoRepositorio.FiltrarPorSerie(command);
                    //break;
                 //case "Aluno":
                //    return conselho = _conselhoRepositorio.FiltrarPorAluno(command);

                //case "Data":
                //    return conselho = _conselhoRepositorio.FiltrarTodosPorData(command);

            //    default:
            //        return null;
            //}

            var obj = TemplateGenerator.ListarConselho(conselho);


            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = ConfiguracaoPdf._GlobalSettings(Orientation.Landscape),
                Objects = { ConfiguracaoPdf._ObjectSettings(obj,"assets", "bootstrap.min.css") }
            };

            //_converter.Convert(pdf); IF WE USE Out PROPERTY IN THE GlobalSettings CLASS, THIS IS ENOUGH FOR CONVERSION

            var file = _converter.Convert(pdf);

            //return Ok("Successfully created PDF document.");
            //return File(file, "application/pdf", "EmployeeReport.pdf"); USE THIS RETURN STATEMENT TO DOWNLOAD GENERATED PDF DOCUMENT
            return File(file, "application/pdf");

            //return conselhos;

        }

    }
}