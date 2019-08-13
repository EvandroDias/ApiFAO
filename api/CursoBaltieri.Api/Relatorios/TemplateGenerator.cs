using Api.Util;
using Domain.ConselhoContent.Commands.Outputs;
using Domain.HorarioRotinaContent.Commands.Outputs;
using Domain.OcorrenciaContent.Commands.Outputs;
using Domain.RotinaContent.Commands.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Relatorios
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString()
        {
            var employees = DataStorage.GetAllEmployess();

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><img src='http://192.168.1.254:5200/imagens/relatorio/Logo.PNG'/></div>
                                <table align='left'>
                                    <tr>
                                        <th>Name</th>
                                        <th>LastName</th>
                                        <th>Age</th>
                                        <th>Gender</th>
                                    </tr>");

            foreach (var emp in employees)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", emp.Name, emp.LastName, emp.Age, emp.Gender);
            }

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }

        public static string ImprimirRotina(ImprimirRotinaResults horarioRotina)
        {

            return HtmlRotina(horarioRotina);
        }

        public static string ListarOcorrencias(List<ListarOcorrenciaResults> ocorrencias)
        {

            return Html(ocorrencias);
        }

        public static string TotalOcorrencias(List<RetornoTotalOcorrenciaResults> ocorrencias)
        {

            return TotalOcorrenciaHtml(ocorrencias);
        }


        public static string ListarConselho(IList<ListarConselhoResults> conselho)
        {

            return HtmlConselho(conselho);
        }

        private static string HtmlConselho(IList<ListarConselhoResults> conselho)
        {
            var sb = new StringBuilder();

            //var grupo = horarioRotina.ListarHorarioRotinaResults.GroupBy(c => c.Aula);

            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>");


            foreach (var emp in conselho)
            {
                sb.AppendFormat(@"<p style='margin-left:40%' class='align='center'>{0} - {1} - {2}</p>", emp.NomeBimestre, emp.DataConselho.ToShortDateString(), emp.NomeSerie);

               

                sb.Append(@"
                                  
                                <table border='1px' id='example' class='table table-sm' cellspacing='0' width='100%' role='grid' aria-describedby='example_info' style='width: 100%; table-layout:fixed'>
                                    <tr>
                                        <th style='width: 350px;text-align: left'>Aluno</th>
                                        <th>Descrição</th>
                                        
                                    </tr>");

                foreach (var item in emp.Alunos)
                {


                    sb.AppendFormat(@"<tr>
                                        <td text-align: left'>{0}</td>
                                        <td>{1}</td>

                                    </tr>", item.NomeAluno != null ? item.NomeAluno.ToUpper() : item.NomeAluno, item.Descricao);
                }

                sb.Append(@"
                                </table>
                            
                        
                                
                           ");

                

                sb.AppendFormat(@"<table class='table table-borderless margemTable' align='center'>
                                  
                                    <td>
                                            <p style='text-align: center'>_________________________________________</p>
                                            <p style='text-align: center;margin-top:-1px;'>{0}</p>
                                    </td>
                                   
                                    <td>
                                            <p style='text-align: center'>_________________________________________</p>
                                            <p style='text-align: center;margin-top:-1;'>{1}</p>
                                    </td>
                                    <td>
                                            <p style='text-align: center'>_________________________________________</p>
                                            <p style='text-align: center;margin-top:0;'>{2}</p>
                                    </td>
                                   
                                  </tr> </table>", emp.NomeDiretor.ToUpper(), emp.NomeCoordenador.ToUpper(), emp.NomeFuncionario.ToUpper());
                

                //sb.Append(@"<div class='form-inline'>" +
                //                    "<div class='form-group'>"+
                //                     "<div class='col-md-3 col-sm-3 col-xs-3'>" +
                //                       "<p>_________________________</p>" +
                //                       "<p><b>" + emp.NomeFuncionario + "</b></p>" +
                //                     "</div>" +
                //                     "</div>"+

                //                      "<div class='form-group'>" +
                //                     "<div class='col-md-3 col-sm-3 col-xs-3'>" +
                //                       "<p>________________________</p>" +
                //                       "<p><b>" + emp.NomeCoordenador + "</b></p>" +
                //                     "</div>" +
                //                     "</div>" +

                //                      "<div class='form-group'>" +
                //                     "<div class='col-md-3 col-sm-3 col-xs-3'>" +
                //                       "<p>__________________________</p>" +
                //                       "<p><b>" + emp.NomeCoordenador + "</b></p>" +
                //                     "</div>" +
                //                     "</div>" +

                //             "</div>");




            }
            sb.Append(@"
                
                 </body>
                        </html>");
            return sb.ToString();
        }

        private static string TotalOcorrenciaHtml(IList<RetornoTotalOcorrenciaResults> ocorrencias)
        {
            var sb = new StringBuilder();
             int cont = 5;

            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                
                                  <!--<img class='card-img-top' src='http://192.168.1.254:5200/imagens/relatorio/Logo.PNG' alt='Card image cap'>-->
                                  
                              <div class='table-responsive'>
                                   <table border='1px' id='example'  class='table' cellspacing='0' width='100%' role='grid' aria-describedby='example_info' style='width: 100%; table-layout:fixed'>
                                  <thead>
                                   <!-- <tr class='fundoTr' align='center'>
                                        <th  width='300px'>ALUNO</th>
                                        <th  colspan='{0}'>Total</th>-->"
                                      );
                                    

            


            foreach (var emp in ocorrencias)
            {
                

                sb.AppendFormat(@" <tbody>
                                    <tr>
                                    <td valign='middle' height='60px' align='center' scope='row'><h5>{0}</h5></td>", emp.NomeAluno.ToUpper());

                foreach (var item in emp.TipoOcorrencia)
                {
                    sb.AppendFormat(@"
                                                                     
                                 <td valign='middle' height='60px' align='center' scope='row'><h4>{0}</h4></td>
                                   
                                ", item);
                                       
                }
            }

            sb.Append(@"
                                     </tr>
                                    </tbody>
                                </table>
                                     </div>                                           
                            </body>
                        </html>");

            return sb.ToString();
        }

        private static string Html(IList<ListarOcorrenciaResults> ocorrencias)
        {
            var sb = new StringBuilder();

            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                
                                  <!--<img class='card-img-top' src='http://192.168.1.254:5200/imagens/relatorio/Logo.PNG' alt='Card image cap'>-->
                                  
                              <div class='table-responsive'>
                                   <table border='1px' id='example' class='display dataTable dtr-inline' cellspacing='0' width='100%' role='grid' aria-describedby='example_info' style='width: 100%; table-layout:fixed'>
                                  <thead>
                                    <tr class='fundoTr' align='left'>
                                        <th width='250px'>ALUNO</th>
                                        <th width='150px'>TÍTULO</th>
                                        <th>OCORRÊNCIA</th>
                                        <th width='80px'>DATA</th>
                                    </tr>
                                  </thead>
                                    ");




            foreach (var emp in ocorrencias)
            {
                sb.AppendFormat(@" <tbody>
                                    <tr>
                                    <th align='left' scope='row'>{0}</td>
                                    <td align='left' class='espaco' >{1}</td>
                                    <td align='left'>{2}</td>
                                    <td align='left'>{3}</td>
                                  </tr></tbody>", emp.NomeAluno.ToUpper(), emp.Titulo, emp.Descricao, emp.DataOcorrencia.ToShortDateString());
            }

            sb.Append(@"
                                </table>
                                     </div>                                           
                            </body>
                        </html>");

            return sb.ToString();
        }

        private static string HtmlRotina(ImprimirRotinaResults horarioRotina)
        {
            var sb = new StringBuilder();

            var grupo = horarioRotina.ListarHorarioRotinaResults.GroupBy(c => c.Aula);


            sb.Append(@"
                        <html>
                            <head>
                                  
                            </head>
                            <body>
                                                              
                                 
                                  <img class='tamnhoImg' src='http://192.168.1.254:5200/imagens/relatorio/Logo.PNG' alt='Card image cap'>
                                    
                                   <h4 class='margemTitulo'><b>Rotina semanal: " + horarioRotina.ListarRotinaResults.De.ToString("dd/MM/yyyy") + " a " + horarioRotina.ListarRotinaResults.Ate.ToString("dd/MM/yyyy") + " - " + horarioRotina.ListarRotinaResults.NomeSerie + " - " + horarioRotina.ListarRotinaResults.NomeFuncionario + "</b></h4>" +
                                   

                                   "<table border='1px' id='example' class='display dataTable dtr-inline' cellspacing='0' width='100%' role='grid' aria-describedby='example_info' style='width: 100%; table-layout:fixed'>" +

                                 "<thead>" +
                                  " <tr>" +
                                     " <th width='60px'  class='table-dark' scope='row'>Horário</th>" +
                                     " <th  class='table-dark' scope='row'>2°FEIRA</th>" +
                                     " <th  class='table-dark' scope='row'>3°FEIRA</th>" +
                                    "  <th  class='table-dark' scope='row'>4°FEIRA</th>" +
                                     " <th  class='table-dark' scope='row'>5°FEIRA</th>" +
                                     " <th  class='table-dark' scope='row'>6°FEIRA</th>" +
                                 "  </tr>" +
                                 " </thead>");







            foreach (var item in grupo)
            {
                sb.Append(@"<tbody><tr>");

                if (item.Key == "1°Aula")
                {

                    sb.Append(@"<td>" + item.Key + "</td>");

                    foreach (var i in item.OrderBy(c => c.Order))
                    {
                        sb.Append(@"<td><div [innerHTML] = " + i.Conteudo + "</div></td>");
                    }

                    sb.Append(@"</tr>");
                }
                else if (item.Key == "2°Aula")
                {
                    sb.Append(@"<td>" + item.Key + "</td>");

                    foreach (var i in item.OrderBy(c => c.Order))
                    {
                        sb.Append(@"<td><div [innerHTML] = " + i.Conteudo + "</div></td>");
                    }

                    sb.Append(@"</tr>");
                }
                else if (item.Key == "3°Aula")
                {
                    sb.Append(@"<td>" + item.Key + "</td>");

                    foreach (var i in item.OrderBy(c => c.Order))
                    {
                        sb.Append(@"<td><div [innerHTML] = " + i.Conteudo + "</div></td>");
                    }

                    sb.Append(@"</tr>");
                }
                else if (item.Key == "4°Aula")
                {
                    sb.Append(@"<td>" + item.Key + "</td>");

                    foreach (var i in item.OrderBy(c => c.Order))
                    {
                        sb.Append(@"<td><div [innerHTML] = " + i.Conteudo + "</div></td>");
                    }

                    sb.Append(@"</tr>");
                }
                else if (item.Key == "5°Aula")
                {
                    sb.Append(@"<td>" + item.Key + "</td>");

                    foreach (var i in item.OrderBy(c => c.Order))
                    {
                        sb.Append(@"<td><div [innerHTML] = " + i.Conteudo + "</ div ></td>");
                    }

                    sb.Append(@"</tr>");
                }
                else
                {
                    sb.Append(@"<td>" + item.Key + "</td>");

                    foreach (var i in item.OrderBy(c => c.Order))
                    {
                        sb.Append(@"<td><div [innerHTML] = " + i.Conteudo + "</div></td>");
                    }

                    sb.Append(@"</tr></tbody>");
                }


            }

            sb.Append(@"
                                </table>
                                  

                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
