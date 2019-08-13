using Api.Relatorios;
using Api.Util;
using DinkToPdf;
using DinkToPdf.Contracts;
using Domain.RotinaContent.Commands.Inputs;
using Domain.RotinaContent.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/RelatorioRotina")]
    public class RelatorioRotinaController : TransacaoBase
    {
        private readonly IRotinaRepositorio _rotinaRepositorio;
        private IConverter _converter;

        public RelatorioRotinaController( IConverter converter, IRotinaRepositorio rotinaRepositorio, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._rotinaRepositorio = rotinaRepositorio;
            _converter = converter;

        }
        [HttpPost]
        [Route("v1/imprimir")]
        public dynamic ImprimirRotina([FromBody] ImprimirRotinaCommands command)
        {

            var rotina = _rotinaRepositorio.Imprimir(command.RotinaId);

            var obj = TemplateGenerator.ImprimirRotina(rotina);

            var pdf = RetornoPdf.Retorno(obj, "assets", "styles.css", Orientation.Landscape);


            //file = _converter.Convert(pdf);


            //var pdf = new HtmlToPdfDocument()
            //{
            //    GlobalSettings = ConfiguracaoPdf._GlobalSettings(Orientation.Landscape),
            //    Objects = { ConfiguracaoPdf._ObjectSettings(obj, "assets", "style.css") }
            //};

            //_converter.Convert(pdf); IF WE USE Out PROPERTY IN THE GlobalSettings CLASS, THIS IS ENOUGH FOR CONVERSION

            var file = _converter.Convert(pdf);

            //return Ok("Successfully created PDF document.");
            //return File(file, "application/pdf", "EmployeeReport.pdf"); USE THIS RETURN STATEMENT TO DOWNLOAD GENERATED PDF DOCUMENT
            return File(file, "application/pdf");

        }
       
    }
}