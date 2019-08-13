using Domain.ConselhoContent.Commands.Inputs;
using Domain.ConselhoContent.Commands.Outputs;
using Domain.ConselhoContent.Entities;
using Domain.ConselhoContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class ConselhoRepositorio : IConselhoRepositorio
    {
        private readonly DataContext _context;

        public ConselhoRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Conselho Salvar(Conselho obj)
        {
            var retorno = _context.Conselhos.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Conselho obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public Conselho Existe(Guid id)
        {
            return _context.Conselhos.Where(s => s.Id == id).FirstOrDefault();
        }

        public Conselho Existe(Guid bimestreId, Guid serieId, Guid anoId)
        {
            return _context.Conselhos.Where(s => s.BimestreId == bimestreId && s.SerieId == serieId && s.AnoId == anoId).FirstOrDefault();
        }

        public IList<ListarConselhoResults> ListarTodos(int skip, int take)
        {
            var retorno = Consulta();

              var r = retorno.OrderBy(o => o.NomeSerie).Skip(skip).Take(take).ToList();

            return r;
        }
        public IList<ListarConselhoResults> ListarPorPerido(DateTime inicio, DateTime final)
        {
            var retorno = Consulta();

            var _retorno = retorno.Where(r => r.DataConselho >= inicio && r.DataConselho <= final && r.Status == true).OrderByDescending(u => u.DataConselho).ToList();
                          

            return _retorno;
        }

        public IList<ListarConselhoResults> ListarPorNomeAluno(string nomeAluno)
        {
            var retorno = Consulta();

            var _retorno = retorno.Where(r => r.NomeAluno == nomeAluno && r.Status == true).OrderByDescending(u => u.DataConselho).ToList();


            return _retorno;
            
        }

        public IList<ListarConselhoResults> ListarPorBimestre(Guid bimestreId)
        {
            var retorno = Consulta();

            var _retorno = retorno.Where(r => r.BimestreId == bimestreId && r.Status == true).OrderByDescending(u => u.DataConselho).ToList();


            return _retorno;
           
        }

        public IList<ListarConselhoResults> ListarPorNomeAlunoBimestre(string nomeAluno, Guid bimestreId)
        {
            var retorno = Consulta();

            var _retorno = retorno.Where(r => r.NomeAluno == nomeAluno && r.BimestreId == bimestreId && r.Status == true).OrderByDescending(u => u.DataConselho).ToList();


            return _retorno;
          
        }
        public IList<ListarConselhoResults> ListarPorData(DateTime data)
        {
            var retorno = Consulta();

            var _retorno = retorno.Where(r => r.DataConselho == data && r.Status == true).OrderByDescending(u => u.DataConselho).ToList();


            return _retorno;
         
        }

      
        public DetalheConselhoResults Detalhes(Guid conselhoId)
        {

            var retorno = (from o in _context.Conselhos
                                          join b in _context.Bimestres on o.BimestreId equals b.Id
                                          join f in _context.Funcionarios on o.FuncionarioId equals f.Id
                                          join s in _context.Series on o.SerieId equals s.Id
                                           join a in _context.Anos on o.AnoId equals a.Id
                                           where o.Id == conselhoId
                           select new DetalheConselhoResults()
                           {
                               ConselhoId = o.Id,
                               DataCadastro = o.DataCadastro,
                               DataConselho = o.DataConselho,
                               FuncionarioId = o.FuncionarioId,
                               Status = o.Status,
                               BimestreId = b.Id,
                               NomeBimestre = b.Nome,
                               NomeAno = a.Nome,
                               NomeDiretor = o.NomeDiretor,
                               NomeFuncionario = f.Nome + " " + f.SobreNome,
                               NomeSerie = s.Nome,
                               SerieId = s.Id,
                               UsuarioId = o.UsuarioId,
                               AnoId = o.AnoId,
                               NomeCoordenador = o.NomeCoordenador

                           }).FirstOrDefault();

            return retorno;
        }

        private IQueryable<ListarConselhoResults> Consulta()
        {
            var retorno = (from o in _context.Conselhos
                           join b in _context.Bimestres on o.BimestreId equals b.Id
                           join f in _context.Funcionarios on o.FuncionarioId equals f.Id
                           join s in _context.Series on o.SerieId equals s.Id
                           join a in _context.Anos on o.AnoId equals a.Id
                           where 1 == 1
             select new ListarConselhoResults()
             {
                 ConselhoId = o.Id,
                 DataCadastro = o.DataCadastro,
                 DataConselho = o.DataConselho,
                 FuncionarioId = o.UsuarioId,
                 Status = o.Status,
                 BimestreId = b.Id,
                 NomeBimestre = b.Nome,
                 NomeFuncionario = f.Nome+" "+f.SobreNome,
                 NomeSerie = s.Nome,
                 NomeAno = a.Nome,
                 SerieId = s.Id,
                 UsuarioId = o.UsuarioId,
                 AnoId = o.AnoId,
                 NomeCoordenador = o.NomeCoordenador,
                                
             });

            return retorno;
        }

        public List<ListarConselhoResults> FiltrarPorSerie(FiltroConselhoCommands command)
        {

            var lista = new List<ListarConselhoResults>();
            //var filtroOcorrencia = new FiltroOcorrenciaResults();

            var retorno = ConsultaRelatorio();

            if (!string.IsNullOrEmpty(command.SerieId) && !string.IsNullOrEmpty(command.AnoId))
            {
                var _retorno = retorno.Where(c => c.SerieId == Guid.Parse(command.SerieId) && c.AnoId == Guid.Parse(command.AnoId)).OrderBy(c=>c.NomeAluno).ToList();

                foreach (var item in _retorno)
                {
                    var r = RetornoConselho(item.ConselhoId);

                    var _lista = new ListarConselhoResults(item.DataConselho, item.DataCadastro, item.NomeSerie, item.NomeAno, item.NomeFuncionario, item.NomeBimestre, item.NomeCoordenador, item.NomeDiretor, r);

                    lista.Add(_lista);
                }
                                          
            }
            else if (!string.IsNullOrEmpty(command.BimestreId) && !string.IsNullOrEmpty(command.AnoId))
            {


                var _retorno = retorno.Where(c => c.BimestreId == Guid.Parse(command.BimestreId) && c.AnoId == Guid.Parse(command.AnoId)).OrderBy(c => c.NomeAluno).ToList();

                foreach (var item in _retorno)
                {
                    var r = RetornoConselho(item.ConselhoId);

                    var _lista = new ListarConselhoResults(item.DataConselho, item.DataCadastro, item.NomeSerie, item.NomeAno, item.NomeFuncionario, item.NomeBimestre, item.NomeCoordenador, item.NomeDiretor, r);

                    lista.Add(_lista);
                }

            }
            else if (!string.IsNullOrEmpty(command.AnoId))
            {
                var _retorno = retorno.Where(c => c.AnoId == Guid.Parse(command.AnoId)).OrderBy(c => c.NomeAluno).ToList();

                foreach (var item in _retorno)
                {
                    var r = RetornoConselho(item.ConselhoId);

                    var _lista = new ListarConselhoResults(item.DataConselho, item.DataCadastro, item.NomeSerie, item.NomeAno, item.NomeFuncionario, item.NomeBimestre, item.NomeCoordenador, item.NomeDiretor, r);

                    lista.Add(_lista);
                }

            }
            else if (!string.IsNullOrEmpty(command.ConselhoId))
            {


                var _retorno = retorno.Where(c => c.ConselhoId == Guid.Parse(command.ConselhoId)).OrderBy(c => c.NomeAluno).ToList();

                foreach (var item in _retorno)
                {
                    var r = RetornoConselho(item.ConselhoId);

                    var _lista = new ListarConselhoResults(item.DataConselho, item.DataCadastro, item.NomeSerie, item.NomeAno, item.NomeFuncionario, item.NomeBimestre, item.NomeCoordenador, item.NomeDiretor, r);

                    lista.Add(_lista);
                }

            }


            return lista.Distinct().ToList();
        }

       
        private List<ListarAlunoConselhoResults> RetornoConselho(Guid conselhoId)
        {
           var retorno = (from ac in _context.AlunoConselhos
             join a in _context.Alunos on ac.AlunoId equals a.Id
             where ac.ConselhoId == conselhoId
             select new ListarAlunoConselhoResults()
             {
                 Id = a.Id,
                 NomeAluno = a.Nome + " " + a.SobreNome,
                 Descricao = ac.Descricao
             }).OrderBy(c => c.NomeAluno).ToList();

            return retorno;
        }

        private IQueryable<ListarConselhoResults> ConsultaRelatorio()
        {
            var retorno = (from o in _context.Conselhos
                           join b in _context.Bimestres on o.BimestreId equals b.Id
                           join f in _context.Funcionarios on o.FuncionarioId equals f.Id
                           join s in _context.Series on o.SerieId equals s.Id
                           join a in _context.Anos on o.AnoId equals a.Id
                           
                           where 1 == 1
                           select new ListarConselhoResults()
                           {
                               ConselhoId = o.Id,
                               DataCadastro = o.DataCadastro,
                               DataConselho = o.DataConselho,
                               FuncionarioId = o.UsuarioId,
                               Status = o.Status,
                               BimestreId = b.Id,
                               NomeBimestre = b.Nome,
                               NomeFuncionario = f.Nome + " " + f.SobreNome,
                               NomeSerie = s.Nome,
                               NomeAno = a.Nome,
                               SerieId = s.Id,
                               UsuarioId = o.UsuarioId,
                               AnoId = o.AnoId,
                               NomeCoordenador = o.NomeCoordenador,
                               NomeDiretor = o.NomeDiretor


                           });

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
