using Domain.OcorrenciaContent.Commands.Inputs;
using Domain.OcorrenciaContent.Commands.Outputs;
using Domain.OcorrenciaContent.Entities;
using Domain.OcorrenciaContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using Shared.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Infra.Repositories
{
    public class OcorrenciaRepositorio : IOcorrenciaRepositorio
    {
        private readonly DataContext _context;

        public OcorrenciaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Ocorrencia Salvar(Ocorrencia obj)
        {
            var retorno = _context.Ocorrencias.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Ocorrencia obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public Ocorrencia Existe(Guid id)
        {
            return _context.Ocorrencias.Where(s => s.Id == id).FirstOrDefault();
        }

        public List<ListarOcorrenciaResults> ListarTodos(int skip, int take)
        {
            var retorno = (from o in _context.Ocorrencias
                           join a in _context.Alunos on o.AlunoId equals a.Id
                           where o.Status == true
                           select new ListarOcorrenciaResults()
                           {
                               OcorrenciaId = o.Id,
                               Titulo = o.Titulo,
                               Descricao = o.Descricao,
                               AlunoId = o.AlunoId,
                               NomeAluno = a.Nome + " " + a.SobreNome,
                               DataCadastro = o.DataCadastro,
                               DataOcorrencia = o.DataOcorrencia,
                               FuncionarioId = o.UsuarioId,
                               Status = o.Status,
                               Visualizada = o.Visualizada
                           }).OrderByDescending(o => o.DataOcorrencia).Skip(skip).Take(take).ToList();

            return retorno;
        }
        public List<ListarOcorrenciaResults> ListarPorPerido(DateTime inicio, DateTime final)
        {
            var retorno = (from o in _context.Ocorrencias
                           join a in _context.Alunos on o.AlunoId equals a.Id
                           where o.DataOcorrencia >= inicio && o.DataOcorrencia <= final && o.Status == true
                           select new ListarOcorrenciaResults()
                           {
                               OcorrenciaId = o.Id,
                               Titulo = o.Titulo,
                               Descricao = o.Descricao,
                               AlunoId = o.AlunoId,
                               NomeAluno = a.Nome + " " + a.SobreNome,
                               DataCadastro = o.DataCadastro,
                               DataOcorrencia = o.DataOcorrencia,
                               FuncionarioId = o.UsuarioId,
                               Status = o.Status,
                               Visualizada = o.Visualizada
                           }).OrderBy(o => o.DataCadastro).ToList();

            return retorno;
        }

        public List<ListarOcorrenciaResults> ListarPorNomeAluno(string nomeAluno)
        {
            var retorno = (from o in _context.Ocorrencias
                           join a in _context.Alunos on o.AlunoId equals a.Id
                           where a.Nome == nomeAluno && o.Status == true
                           select new ListarOcorrenciaResults()
                           {
                               OcorrenciaId = o.Id,
                               Titulo = o.Titulo,
                               Descricao = o.Descricao,
                               AlunoId = o.AlunoId,
                               NomeAluno = a.Nome + " " + a.SobreNome,
                               DataCadastro = o.DataCadastro,
                               DataOcorrencia = o.DataOcorrencia,
                               FuncionarioId = o.UsuarioId,
                               Status = o.Status,
                               Visualizada = o.Visualizada
                           }).OrderBy(o => o.DataCadastro).ToList();

            return retorno;
        }

        public List<ListarOcorrenciaResults> ListarPorTipoOcorrencia(Guid tipoOcorrencia)
        {
            var retorno = (from o in _context.Ocorrencias
                           join a in _context.Alunos on o.AlunoId equals a.Id
                           join t in _context.TipoOcorrencias on o.TipoOcorrenciaId equals t.Id
                           where t.Id == tipoOcorrencia && o.Status == true
                           select new ListarOcorrenciaResults()
                           {
                               OcorrenciaId = o.Id,
                               Titulo = o.Titulo,
                               Descricao = o.Descricao,
                               AlunoId = o.AlunoId,
                               NomeAluno = a.Nome + " " + a.SobreNome,
                               DataCadastro = o.DataCadastro,
                               DataOcorrencia = o.DataOcorrencia,
                               FuncionarioId = o.UsuarioId,
                               Status = o.Status,
                               Visualizada = o.Visualizada
                           }).OrderBy(o => o.DataCadastro).ToList();

            return retorno;
        }

        public List<ListarOcorrenciaResults> ListarPorNomeAlunoTipoOcorrenica(string nomeAluno, Guid tipoOcorrencia)
        {
            var retorno = (from o in _context.Ocorrencias
                           join a in _context.Alunos on o.AlunoId equals a.Id
                           join t in _context.TipoOcorrencias on o.TipoOcorrenciaId equals t.Id
                           where a.Nome + " " + a.SobreNome == nomeAluno && t.Id == tipoOcorrencia && o.Status == true
                           select new ListarOcorrenciaResults()
                           {
                               OcorrenciaId = o.Id,
                               Titulo = o.Titulo,
                               Descricao = o.Descricao,
                               AlunoId = o.AlunoId,
                               NomeAluno = a.Nome + " " + a.SobreNome,
                               DataCadastro = o.DataCadastro,
                               DataOcorrencia = o.DataOcorrencia,
                               FuncionarioId = o.UsuarioId,
                               Status = o.Status,
                               Visualizada = o.Visualizada
                           }).OrderBy(o => o.DataCadastro).ToList();

            return retorno;
        }
        public List<ListarOcorrenciaResults> ListarPorData(DateTime data)
        {
            var retorno = (from o in _context.Ocorrencias
                           join a in _context.Alunos on o.AlunoId equals a.Id
                           where o.DataOcorrencia == data && o.Status == true
                           select new ListarOcorrenciaResults()
                           {
                               OcorrenciaId = o.Id,
                               Titulo = o.Titulo,
                               Descricao = o.Descricao,
                               AlunoId = o.AlunoId,
                               NomeAluno = a.Nome + " " + a.SobreNome,
                               DataCadastro = o.DataCadastro,
                               DataOcorrencia = o.DataOcorrencia,
                               FuncionarioId = o.UsuarioId,
                               Status = o.Status,
                               Visualizada = o.Visualizada
                           }).OrderBy(o => o.DataCadastro).ToList();

            return retorno;
        }

        public List<ListarOcorrenciaResults> ListarMinhasOcorrencias(int skip, int take, Guid usuarioId)
        {
            var retorno = (from o in _context.Ocorrencias
                           join a in _context.Alunos on o.AlunoId equals a.Id
                           where o.UsuarioId == usuarioId && o.Status == true
                           select new ListarOcorrenciaResults()
                           {
                               OcorrenciaId = o.Id,
                               Titulo = o.Titulo,
                               Descricao = o.Descricao,
                               AlunoId = o.AlunoId,
                               NomeAluno = a.Nome + " " + a.SobreNome,
                               DataCadastro = o.DataCadastro,
                               DataOcorrencia = o.DataOcorrencia,
                               FuncionarioId = o.UsuarioId,
                               Status = o.Status,
                               Visualizada = o.Visualizada
                           }).OrderBy(o => o.DataCadastro).Skip(skip).Take(take).ToList();

            return retorno;
        }

        public DetalheOcorrenciaResults Detalhes(Guid ocorrenciaId)
        {
            var retorno = (from o in _context.Ocorrencias
                           join a in _context.Alunos on o.AlunoId equals a.Id
                           join t in _context.TipoOcorrencias on o.TipoOcorrenciaId equals t.Id
                           join s in _context.Series on o.SerieId equals s.Id
                           join f in _context.Funcionarios on o.FuncionarioId equals f.Id
                           where o.Id == ocorrenciaId && o.Status == true
                           select new DetalheOcorrenciaResults()
                           {
                               OcorrenciaId = o.Id,
                               Titulo = o.Titulo,
                               Descricao = o.Descricao,
                               AlunoId = o.AlunoId,
                               SerieId = o.SerieId,
                               DataCadastro = o.DataCadastro,
                               DataOcorrencia = o.DataOcorrencia.ToString("dd/MM/yyyy"),
                               FuncionarioId = o.FuncionarioId,
                               Periodo = o.Periodo,
                               TipoOcorrenciaId = o.TipoOcorrenciaId,
                               UsuarioId = o.UsuarioId,
                               Status = o.Status,
                               Visualizada = o.Visualizada,
                               NomeAluno = a.Nome + " " + a.SobreNome,
                               NomeFuncionario = f.Nome + " " + f.SobreNome,
                               NomeSerie = s.Nome,
                               NomeTipoOcorrencia = t.Nome
                           }).FirstOrDefault();

            return retorno;
        }


        public PainelGraficoOcorrenciaResults GraficoPainel(FiltroOcorrenciaCommands command)
        {
            var graficoOcorrenciaResults = new PainelGraficoOcorrenciaResults();


            var retornoSerie = (from o in _context.Ocorrencias
                                join s in _context.Series on o.SerieId equals s.Id
                                where 1 == 1
                                select new ListarOcorrenciaResults()
                                {
                                    OcorrenciaId = o.Id,
                                    Titulo = o.Titulo,
                                    Descricao = o.Descricao,
                                    AlunoId = o.AlunoId,
                                    DataCadastro = o.DataCadastro,
                                    DataOcorrencia = o.DataOcorrencia,
                                    FuncionarioId = o.UsuarioId,
                                    Status = o.Status,
                                    SerieId = o.SerieId,
                                    TipoOcorrenciaId = o.TipoOcorrenciaId,
                                    Visualizada = o.Visualizada,
                                    NomeSerie = s.Nome,
                                });

            var retornoAluno = (from o in _context.Ocorrencias
                                join a in _context.Alunos on o.AlunoId equals a.Id
                                where 1 == 1
                                select new ListarOcorrenciaResults()
                                {
                                    OcorrenciaId = o.Id,
                                    Titulo = o.Titulo,
                                    Descricao = o.Descricao,
                                    AlunoId = o.AlunoId,
                                    DataCadastro = o.DataCadastro,
                                    DataOcorrencia = o.DataOcorrencia,
                                    FuncionarioId = o.UsuarioId,
                                    Status = o.Status,
                                    SerieId = o.SerieId,
                                    TipoOcorrenciaId = o.TipoOcorrenciaId,
                                    Visualizada = o.Visualizada,
                                    NomeAluno = a.Nome + " " + a.SobreNome
                                });



            if (command.TipoOcorrenciaId == null || command.TipoOcorrenciaId == "Todos")
            {

                if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                {

                    var _date = Convert.ToDateTime(command.De);

                    var ate = _date.AddDays(Convert.ToDouble(command.Dias));


                    retornoSerie = retornoSerie.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= ate && r.Status == true).OrderBy(r => r.DataOcorrencia);
                    retornoAluno = retornoAluno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= ate && r.Status == true).OrderBy(r => r.DataOcorrencia);

                    var data = retornoSerie.ToList();
                    var dataAluno = retornoAluno.ToList();

                    var serie = Agrupar(data, 1, true);
                    var aluno = AgruparAluno(dataAluno, 1, true);

                    graficoOcorrenciaResults.LineChartDataSerie = serie.LineChartData;
                    graficoOcorrenciaResults.MesSerie = serie.Mes;

                    graficoOcorrenciaResults.LineChartDataAluno = aluno.LineChartData;
                    graficoOcorrenciaResults.MesAluno = aluno.Mes;

                }

            }
            else
            {
                if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)))
                {

                    var _date = Convert.ToDateTime(command.De);

                    var ate = _date.AddDays(Convert.ToDouble(command.Dias));


                    retornoSerie = retornoSerie.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= ate && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderBy(r => r.DataOcorrencia);
                    retornoAluno = retornoAluno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= ate && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderBy(r => r.DataOcorrencia);

                    var data = retornoSerie.ToList();
                    var dataAluno = retornoAluno.ToList();

                    var serie = Agrupar(data, 1, true);
                    var aluno = AgruparAluno(dataAluno, 1, true);

                    graficoOcorrenciaResults.LineChartDataSerie = serie.LineChartData;
                    graficoOcorrenciaResults.MesSerie = serie.Mes;

                    graficoOcorrenciaResults.LineChartDataAluno = aluno.LineChartData;
                    graficoOcorrenciaResults.MesAluno = aluno.Mes;

                }
            }

            return graficoOcorrenciaResults;
        }

        public PainelGraficoOcorrenciaResults GraficoPorSerie(FiltroOcorrenciaCommands command)
        {
            var graficoOcorrenciaResults = new PainelGraficoOcorrenciaResults();


            var retornoSerie = (from o in _context.Ocorrencias
                                join s in _context.Series on o.SerieId equals s.Id
                                where 1 == 1
                                select new ListarOcorrenciaResults()
                                {
                                    OcorrenciaId = o.Id,
                                    Titulo = o.Titulo,
                                    Descricao = o.Descricao,
                                    AlunoId = o.AlunoId,
                                    DataCadastro = o.DataCadastro,
                                    DataOcorrencia = o.DataOcorrencia,
                                    FuncionarioId = o.UsuarioId,
                                    Status = o.Status,
                                    SerieId = o.SerieId,
                                    TipoOcorrenciaId = o.TipoOcorrenciaId,
                                    Visualizada = o.Visualizada,
                                    NomeSerie = s.Nome
                                });

            var retornoAluno = (from o in _context.Ocorrencias
                                join a in _context.Alunos on o.AlunoId equals a.Id
                                where 1 == 1
                                select new ListarOcorrenciaResults()
                                {
                                    OcorrenciaId = o.Id,
                                    Titulo = o.Titulo,
                                    Descricao = o.Descricao,
                                    AlunoId = o.AlunoId,
                                    DataCadastro = o.DataCadastro,
                                    DataOcorrencia = o.DataOcorrencia,
                                    FuncionarioId = o.UsuarioId,
                                    Status = o.Status,
                                    SerieId = o.SerieId,
                                    TipoOcorrenciaId = o.TipoOcorrenciaId,
                                    Visualizada = o.Visualizada,
                                    NomeAluno = a.Nome + " " + a.SobreNome
                                });

            if (command.TipoFiltro == "Série")
            {
                if (command.SerieId != null)
                {
                    if (command.TipoOcorrenciaId == null || command.TipoOcorrenciaId == "Todos")
                    {

                        if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                        {
                            retornoSerie = retornoSerie.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.Status == true && r.SerieId == Guid.Parse(command.SerieId)).OrderBy(r => r.DataOcorrencia);
                            retornoAluno = retornoAluno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.Status == true && r.SerieId == Guid.Parse(command.SerieId)).OrderBy(r => r.DataOcorrencia);

                            // retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.SerieId == Guid.Parse(command.SerieId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

                            var data = retornoSerie.ToList();
                            var dataAluno = retornoAluno.ToList();

                            var serie = Agrupar(data);
                            var aluno = AgruparAluno(dataAluno, 0, false, 0);

                            graficoOcorrenciaResults.LineChartDataSerie = serie.LineChartData;
                            graficoOcorrenciaResults.MesSerie = serie.Mes;

                            graficoOcorrenciaResults.LineChartDataAluno = aluno.LineChartData;
                            graficoOcorrenciaResults.MesAluno = aluno.Mes;

                            //var data = retorno.ToList();

                            //graficoOcorrenciaResults = Agrupar(data);


                        }


                    }
                    else
                    {

                        if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                        {
                            retornoSerie = retornoSerie.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true && r.SerieId == Guid.Parse(command.SerieId)).OrderBy(r => r.DataOcorrencia);
                            retornoAluno = retornoAluno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true && r.SerieId == Guid.Parse(command.SerieId)).OrderBy(r => r.DataOcorrencia);

                            // retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.SerieId == Guid.Parse(command.SerieId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

                            var data = retornoSerie.ToList();
                            var dataAluno = retornoAluno.ToList();

                            var serie = Agrupar(data);
                            var aluno = AgruparAluno(dataAluno, 0, false, 0);

                            graficoOcorrenciaResults.LineChartDataSerie = serie.LineChartData;
                            graficoOcorrenciaResults.MesSerie = serie.Mes;

                            graficoOcorrenciaResults.LineChartDataAluno = aluno.LineChartData;
                            graficoOcorrenciaResults.MesAluno = aluno.Mes;

                            //var data = retorno.ToList();

                            //graficoOcorrenciaResults = Agrupar(data);


                        }



                    }
                    //else
                    //{


                    //    if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                    //    {

                    //        retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.SerieId == Guid.Parse(command.SerieId) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

                    //        var data = retorno.ToList();


                    //        graficoOcorrenciaResults = Agrupar(data);


                    //    }


                    //}

                }
            }
            //else if (command.TipoFiltro == "Aluno")
            //{
            //    if (command.AlunoId != null)
            //    {
            //        if (command.TipoOcorrenciaId == null || command.TipoOcorrenciaId == "Todos")
            //        {

            //            if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
            //            {

            //                retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.AlunoId == Guid.Parse(command.AlunoId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

            //                var data = retorno.ToList();

            //                graficoOcorrenciaResults = Agrupar(data);


            //            }


            //        }
            //        else
            //        {


            //            if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
            //            {

            //                retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.AlunoId == Guid.Parse(command.AlunoId) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

            //                var data = retorno.ToList();


            //                graficoOcorrenciaResults = Agrupar(data);


            //            }


            //        }
            //    }
            //}
            //else
            //{
            //    if (command.TipoOcorrenciaId == null || command.TipoOcorrenciaId == "Todos")
            //    {

            //        if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
            //        {

            //            retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

            //            var data = retorno.ToList();

            //            graficoOcorrenciaResults = Agrupar(data);

            //        }

            //    }
            //    else
            //    {


            //        if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
            //        {

            //            retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

            //            var data = retorno.ToList();


            //            graficoOcorrenciaResults = Agrupar(data);


            //        }


            //    }


            //}


            return graficoOcorrenciaResults;
        }

        public GraficoOcorrenciaResults GraficoPorData(FiltroOcorrenciaCommands command)
        {
            var graficoOcorrenciaResults = new GraficoOcorrenciaResults();


            var retorno = (from o in _context.Ocorrencias
                           join s in _context.Series on o.SerieId equals s.Id
                           where 1 == 1
                           select new ListarOcorrenciaResults()
                           {
                               OcorrenciaId = o.Id,
                               Titulo = o.Titulo,
                               Descricao = o.Descricao,
                               AlunoId = o.AlunoId,
                               DataCadastro = o.DataCadastro,
                               DataOcorrencia = o.DataOcorrencia,
                               FuncionarioId = o.UsuarioId,
                               Status = o.Status,
                               SerieId = o.SerieId,
                               TipoOcorrenciaId = o.TipoOcorrenciaId,
                               Visualizada = o.Visualizada,
                               NomeSerie = s.Nome
                           });

            if (command.TipoFiltro == "Série")
            {
                if (command.SerieId != null)
                {
                    if (command.TipoOcorrenciaId == null || command.TipoOcorrenciaId == "Todos")
                    {

                        if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                        {

                            retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.SerieId == Guid.Parse(command.SerieId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

                            var data = retorno.ToList();

                            graficoOcorrenciaResults = Agrupar(data);


                        }


                    }
                    else
                    {


                        if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                        {

                            retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.SerieId == Guid.Parse(command.SerieId) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

                            var data = retorno.ToList();


                            graficoOcorrenciaResults = Agrupar(data);


                        }


                    }

                }
            }
            else if (command.TipoFiltro == "Aluno")
            {
                if (command.AlunoId != null)
                {
                    if (command.TipoOcorrenciaId == null || command.TipoOcorrenciaId == "Todos")
                    {

                        if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                        {

                            retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.AlunoId == Guid.Parse(command.AlunoId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

                            var data = retorno.ToList();

                            graficoOcorrenciaResults = Agrupar(data);


                        }


                    }
                    else
                    {


                        if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                        {

                            retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.AlunoId == Guid.Parse(command.AlunoId) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

                            var data = retorno.ToList();


                            graficoOcorrenciaResults = Agrupar(data);


                        }


                    }
                }
            }
            else
            {
                if (command.TipoOcorrenciaId == null || command.TipoOcorrenciaId == "Todos")
                {

                    if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                    {

                        retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

                        var data = retorno.ToList();

                        graficoOcorrenciaResults = Agrupar(data);

                    }

                }
                else
                {


                    if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                    {

                        retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

                        var data = retorno.ToList();


                        graficoOcorrenciaResults = Agrupar(data);


                    }


                }


            }


            return graficoOcorrenciaResults;
        }

        public List<RetornoTotalOcorrenciaResults> RetornoTotalOcorrencia(FiltroOcorrenciaCommands command)
        {
            var retornoTotalOcorrenciaResults = new List<RetornoTotalOcorrenciaResults>();

            var retorno = (from o in _context.Ocorrencias
                           join a in _context.Alunos on o.AlunoId equals a.Id
                           join t in _context.TipoOcorrencias on o.TipoOcorrenciaId equals t.Id
                            where 1 == 1
                           select new ListarOcorrenciaResults()
                           {
                               OcorrenciaId = o.Id,
                               Titulo = o.Titulo,
                               Descricao = o.Descricao,
                               AlunoId = o.AlunoId,
                               NomeAluno = a.Nome + " " + a.SobreNome,
                               DataCadastro = o.DataCadastro,
                               DataOcorrencia = o.DataOcorrencia,
                               FuncionarioId = o.UsuarioId,
                               Status = o.Status,
                               SerieId = o.SerieId,
                               TipoOcorrenciaId = o.TipoOcorrenciaId,
                               Visualizada = o.Visualizada,
                               NomeTipoOcorrencia = t.Nome,
                               
                           });



            var _retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.SerieId == Guid.Parse(command.SerieId) && r.Status == true).OrderByDescending(r => r.NomeAluno).ToList();

            var grupoAluno = _retorno.GroupBy(s => s.NomeAluno).Select(g => new
            {
                Chave = g.Key,
                Lista = g.ToList(),
                Soma = g.GroupBy(c => c.NomeTipoOcorrencia).Count()

            });

            foreach (var item in grupoAluno)
            {


                var qtd = item.Lista.GroupBy(c => c.NomeTipoOcorrencia).Select(g => new
                {
                    Chave = g.Key,
                    Lista = g.ToList(),
                    Soma = g.Count()

                });

                var lista = new List<String>();

                foreach (var x in qtd)
                {

                    var tipo = x.Soma.ToString() + " " + "Ocorrência:" + " " + x.Chave;
                    lista.Add(tipo);

                }

                var retornoTotal = new RetornoTotalOcorrenciaResults(item.Chave, lista);

                retornoTotalOcorrenciaResults.Add(retornoTotal);
            }


            return retornoTotalOcorrenciaResults;

        }

        public List<ListarOcorrenciaResults> FiltrarPorSerie(FiltroOcorrenciaCommands command)
        {
            //var filtroOcorrencia = new FiltroOcorrenciaResults();

            var retorno = this.Consulta();

            if (command.SerieId != null)
            {
                if (command.TipoOcorrenciaId == null || command.TipoOcorrenciaId == "Todos")
                {
                    if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {

                        retorno = retorno.Where(r => r.DataOcorrencia == Convert.ToDateTime(command.De) && r.SerieId == Guid.Parse(command.SerieId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                    {

                        retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.SerieId == Guid.Parse(command.SerieId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {


                        retorno = retorno.Where(r => r.SerieId == Guid.Parse(command.SerieId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                }
                else
                {
                    if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {


                        retorno = retorno.Where(r => r.DataOcorrencia == Convert.ToDateTime(command.De) && r.SerieId == Guid.Parse(command.SerieId) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                    {


                        retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.SerieId == Guid.Parse(command.SerieId) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {

                        retorno = retorno.Where(r => r.SerieId == Guid.Parse(command.SerieId) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();


                    }
                }


            }

            return null;
        }

        public List<ListarOcorrenciaResults> FiltrarTodoPorData(FiltroOcorrenciaCommands command)
        {

            try
            {


                var retorno = this.Consulta();




                if (command.TipoOcorrenciaId == null || command.TipoOcorrenciaId == "Todos")
                {
                    if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {

                        var de = Convert.ToDateTime(command.De);

                        retorno = retorno.Where(r => r.DataOcorrencia == de && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                    {
                        var de = Convert.ToDateTime(command.De);
                        var ate = Convert.ToDateTime(command.Ate);
                        retorno = retorno.Where(r => r.DataOcorrencia >= de && r.DataOcorrencia <= ate && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {

                        retorno = retorno.Where(r => r.AlunoId == Guid.Parse(command.AlunoId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();


                    }
                }
                else
                {
                    if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {


                        retorno = retorno.Where(r => r.DataOcorrencia == Convert.ToDateTime(command.De) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                    {


                        retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {

                        retorno = retorno.Where(r => r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();


                    }


                }

                return null;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ListarOcorrenciaResults> FiltrarPorAluno(FiltroOcorrenciaCommands command)
        {
            //var filtroOcorrencia = new FiltroOcorrenciaResults();

            var retorno = this.Consulta();

            if (command.AlunoId != null)
            {
                if (command.TipoOcorrenciaId == null || command.TipoOcorrenciaId == "Todos")
                {
                    if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {

                        retorno = retorno.Where(r => r.DataOcorrencia == Convert.ToDateTime(command.De) && r.AlunoId == Guid.Parse(command.AlunoId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                    {

                        retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.AlunoId == Guid.Parse(command.AlunoId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {


                        retorno = retorno.Where(r => r.AlunoId == Guid.Parse(command.AlunoId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                }
                else
                {
                    if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {


                        retorno = retorno.Where(r => r.DataOcorrencia == Convert.ToDateTime(command.De) && r.AlunoId == Guid.Parse(command.AlunoId) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.De)) && (command.Ate != "01/01/0001 00:00:00" && !string.IsNullOrEmpty(command.Ate)))
                    {


                        retorno = retorno.Where(r => r.DataOcorrencia >= Convert.ToDateTime(command.De) && r.DataOcorrencia <= Convert.ToDateTime(command.Ate) && r.AlunoId == Guid.Parse(command.AlunoId) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.DataOcorrencia);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();

                    }
                    else if ((command.De == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.De)) && (command.Ate == "01/01/0001 00:00:00" || string.IsNullOrEmpty(command.Ate)))
                    {

                        retorno = retorno.Where(r => r.AlunoId == Guid.Parse(command.AlunoId) && r.TipoOcorrenciaId == Guid.Parse(command.TipoOcorrenciaId) && r.Status == true).OrderByDescending(r => r.NomeAluno);

                        return retorno.ToList();
                        //filtroOcorrencia.Quantidade = retorno.Count();


                    }
                }


            }

            return null;
        }

        public List<ListarOcorrenciaResults> Filtrar(FiltroOcorrenciaCommands command)
        {
            if (command.TipoFiltro == "Série")
            {
                return this.FiltrarPorSerie(command);
            }
            else if (command.TipoFiltro == "Aluno")
            {

                return this.FiltrarPorAluno(command);
            }
            else
            {

                return this.FiltrarTodoPorData(command);
            }
        }

        private GraficoOcorrenciaResults AgruparAluno(List<ListarOcorrenciaResults> data, int qtd = 1, bool ativar = false, int take = 10)
        {
            var graficoOcorrenciaResults = new GraficoOcorrenciaResults();


            var grupoAluno = data.GroupBy(s => s.AlunoId).Select(g => new
            {
                Chave = g.Key,
                Lista = g.ToList(),
                Soma = g.Count(),

            });



            if (take > 0)
            {
                var alunos = grupoAluno.OrderByDescending(c => c.Lista.Count()).Take(take);

                foreach (var item in alunos)
                {

                    var grupo = item.Lista.OrderBy(c => c.DataOcorrencia).GroupBy(c => DateTimeFormatInfo.CurrentInfo.GetMonthName(c.DataOcorrencia.Month).ToLower()).Take(1);

                    var line = new LineChartData();



                    foreach (var y in grupo)
                    {

                        var mes = y.Key.ToUpper();
                        line.Data.Add(y.Count());
                        line.Label = y.Select(c => c.NomeAluno).FirstOrDefault();

                        var existe = graficoOcorrenciaResults.Mes.IndexOf(mes);


                        if (ativar)
                        {
                            if (qtd > graficoOcorrenciaResults.Mes.Count())
                            {
                                var _data = DataBrasilia.HorarioBrasilia();
                                var d = DateTimeFormatInfo.CurrentInfo.GetMonthName(_data.Month).ToLower();
                                graficoOcorrenciaResults.Mes.Add(d);
                            }
                        }
                        else
                        {
                            if (existe == -1)
                                graficoOcorrenciaResults.Mes.Add(mes);
                        }


                    }
                    graficoOcorrenciaResults.LineChartData.Add(line);

                }
            }
            else
            {
                var alunos = grupoAluno.OrderByDescending(c => c.Lista.Count());

                foreach (var item in alunos)
                {

                    var grupo = item.Lista.OrderBy(c => c.DataOcorrencia).GroupBy(c => DateTimeFormatInfo.CurrentInfo.GetMonthName(c.DataOcorrencia.Month).ToLower()).Take(1);

                    var line = new LineChartData();



                    foreach (var y in grupo)
                    {

                        var mes = y.Key.ToUpper();
                        line.Data.Add(y.Count());
                        line.Label = y.Select(c => c.NomeAluno).FirstOrDefault();

                        var existe = graficoOcorrenciaResults.Mes.IndexOf(mes);


                        if (ativar)
                        {
                            if (qtd > graficoOcorrenciaResults.Mes.Count())
                            {
                                var _data = DataBrasilia.HorarioBrasilia();
                                var d = DateTimeFormatInfo.CurrentInfo.GetMonthName(_data.Month).ToLower();
                                graficoOcorrenciaResults.Mes.Add(d);
                            }
                        }
                        else
                        {
                            if (existe == -1)
                                graficoOcorrenciaResults.Mes.Add(mes);
                        }


                    }
                    graficoOcorrenciaResults.LineChartData.Add(line);

                }
            }





            return graficoOcorrenciaResults;
        }

        private GraficoOcorrenciaResults Agrupar(List<ListarOcorrenciaResults> data, int qtd = 1, bool ativar = false)
        {
            var graficoOcorrenciaResults = new GraficoOcorrenciaResults();

            var grupoSerie = data.GroupBy(s => s.SerieId).Select(g => new
            {
                Chave = g.Key,
                Lista = g.ToList(),
                Soma = g.Count(),

            });

            foreach (var item in grupoSerie.OrderByDescending(c => c.Lista.Count()))
            {



                var grupo = item.Lista.OrderBy(c => c.DataOcorrencia).GroupBy(c => DateTimeFormatInfo.CurrentInfo.GetMonthName(c.DataOcorrencia.Month).ToLower());

                var line = new LineChartData();
                foreach (var y in grupo)
                {

                    var mes = y.Key.ToUpper();
                    line.Data.Add(y.Count());
                    line.Label = y.Select(c => c.NomeSerie).FirstOrDefault();

                    var existe = graficoOcorrenciaResults.Mes.IndexOf(mes);

                    if (ativar)
                    {
                        if (qtd > graficoOcorrenciaResults.Mes.Count())
                        {
                            var _data = DataBrasilia.HorarioBrasilia();
                            var d = DateTimeFormatInfo.CurrentInfo.GetMonthName(_data.Month).ToLower();
                            graficoOcorrenciaResults.Mes.Add(d);
                        }
                    }
                    else
                    {
                        if (existe == -1)
                            graficoOcorrenciaResults.Mes.Add(mes);
                    }

                }
                graficoOcorrenciaResults.LineChartData.Add(line);

            }

            return graficoOcorrenciaResults;
        }

        private IQueryable<ListarOcorrenciaResults> Consulta()
        {
            var retorno = (from o in _context.Ocorrencias
                           join a in _context.Alunos on o.AlunoId equals a.Id
                           where 1 == 1
                           select new ListarOcorrenciaResults()
                           {
                               OcorrenciaId = o.Id,
                               Titulo = o.Titulo,
                               Descricao = o.Descricao,
                               AlunoId = o.AlunoId,
                               NomeAluno = a.Nome + " " + a.SobreNome,
                               DataCadastro = o.DataCadastro,
                               DataOcorrencia = o.DataOcorrencia,
                               FuncionarioId = o.UsuarioId,
                               Status = o.Status,
                               SerieId = o.SerieId,
                               TipoOcorrenciaId = o.TipoOcorrenciaId,
                               Visualizada = o.Visualizada
                           }).OrderBy(c => c.DataOcorrencia);

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
