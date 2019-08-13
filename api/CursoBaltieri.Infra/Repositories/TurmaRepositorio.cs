using Domain.TurmaContent.Commands.Outputs;
using Domain.TurmaContent.Entities;
using Domain.TurmaContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class TurmaRepositorio : ITurmaRepositorio
    {
        private readonly DataContext _context;

        public TurmaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Turma Salvar(Turma obj)
        {
            var retorno = _context.Turmas.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Turma obj)
        {
            _context.Entry<Turma>(obj).State = EntityState.Modified;
        }

        public Turma Existe(Guid id)
        {
            return _context.Turmas.Where(s => s.Id == id).FirstOrDefault();
        }

        public Turma Existe(Guid professorId, Guid anoId)
        {
            return _context.Turmas.Where(s => s.FuncionarioId == professorId && s.AnoId == anoId && s.Status == true).FirstOrDefault();
        }

        public Turma ExistePorSerie(Guid serieId)
        {
            return _context.Turmas.Where(s => s.SerieId == serieId).FirstOrDefault();
        }

        public DetalheTurmaResults Detalhes(Guid serieId)
        {
            var retorno = (from t in _context.Turmas
                           join f in _context.Funcionarios on t.FuncionarioId equals f.Id
                           join s in _context.Series on t.SerieId equals s.Id
                           join a in _context.Anos on t.AnoId equals a.Id
                           join d in _context.Departamentos on t.DepartamentoId equals d.Id
                           join e in _context.Escolas on t.EscolaId equals e.Id
                           where t.Id == serieId
                           select new DetalheTurmaResults()
                           {
                              TurmaId = t.Id,
                              EscolaId = e.Id.ToString(),
                              AnoId = a.Id.ToString(),
                              DepartamentoId = d.Id.ToString(),
                              NomeCoordenador = t.Coordenador,
                              NomeDiretor = t.Diretor,
                              Ensino = t.Ensino,
                              FuncionarioId = f.Id.ToString(),
                              NomeAno = a.Nome,
                              NomeEscola = e.Nome,
                              NomeProfessor = f.Nome+" "+f.SobreNome,
                              NomeSala = d.Nome,
                              NomeSerie = s.Nome,
                              Periodo = t.Periodo,
                              QtdAulas1Bimestre = t.QtdAulas1Bimestre,
                              QtdAulas2Bimestre = t.QtdAulas2Bimestre,
                              QtdAulas3Bimestre = t.QtdAulas3Bimestre,
                              QtdAulas4Bimestre = t.QtdAulas4Bimestre,
                              SerieId = s.Id.ToString(),
                              UsuarioId = t.UsuarioId.ToString()
                           }).FirstOrDefault();

            return retorno;
        }
        public IList<ListarTurmaResults> Listar(Boolean status)
        {
            var retorno = (from t in _context.Turmas
                           join f in _context.Funcionarios on t.FuncionarioId equals f.Id
                           join s in _context.Series on t.SerieId equals s.Id
                           join a in _context.Anos on t.AnoId equals a.Id
                           join d in _context.Departamentos on t.DepartamentoId equals d.Id
                           join e in _context.Escolas on t.EscolaId equals e.Id
                           where s.Status == status
                           select new ListarTurmaResults()
                           {
                               TurmaId = t.Id,
                               EscolaId = e.Id.ToString(),
                               AnoId = a.Id.ToString(),
                               DepartamentoId = d.Id.ToString(),
                               Coordenador = t.Coordenador,
                               Diretor = t.Diretor,
                               Ensino = t.Ensino,
                               FuncionarioId = f.Id.ToString(),
                               NomeAno = a.Nome,
                               NomeEscola = e.Nome,
                               NomeProfessor = f.Nome + " " + f.SobreNome,
                               NomeSala = d.Nome,
                               NomeSerie = s.Nome,
                               Periodo = t.Periodo,
                               QtdAulas1Bimestre = t.QtdAulas1Bimestre,
                               QtdAulas2Bimestre = t.QtdAulas2Bimestre,
                               QtdAulas3Bimestre = t.QtdAulas3Bimestre,
                               QtdAulas4Bimestre = t.QtdAulas4Bimestre,
                               SerieId = s.Id.ToString(),
                               UsuarioId = t.UsuarioId.ToString()
                           }).OrderBy(t => t.NomeSerie).ToList();

            return retorno;
        }

        public IList<ListarTurmaResults> ListarPorId(Guid turmaId,Guid anoId)
        {
            var retorno = (from t in _context.Turmas
                           join f in _context.Funcionarios on t.FuncionarioId equals f.Id
                           join s in _context.Series on t.SerieId equals s.Id
                           join a in _context.Anos on t.AnoId equals a.Id
                           join d in _context.Departamentos on t.DepartamentoId equals d.Id
                           join e in _context.Escolas on t.EscolaId equals e.Id
                           where s.Status == true && t.Id == turmaId && a.Id == anoId
                           select new ListarTurmaResults()
                           {
                               TurmaId = t.Id,
                               NomeSerie = s.Nome
                                
                           }).OrderBy(t => t.NomeSerie).ToList();

            return retorno;
        }

        public IList<ListarTurmaResults> ListarTodos(Boolean status,int skip,int take,Guid anoId)
        {
            var retorno = (from t in _context.Turmas
                           join f in _context.Funcionarios on t.FuncionarioId equals f.Id
                           join s in _context.Series on t.SerieId equals s.Id
                           join a in _context.Anos on t.AnoId equals a.Id
                           join d in _context.Departamentos on t.DepartamentoId equals d.Id
                           join e in _context.Escolas on t.EscolaId equals e.Id
                           where s.Status == status && t.AnoId == anoId
                           select new ListarTurmaResults()
                           {
                               TurmaId = t.Id,
                               EscolaId = e.Id.ToString(),
                               AnoId = a.Id.ToString(),
                               DepartamentoId = d.Id.ToString(),
                               Coordenador = t.Coordenador,
                               Diretor = t.Diretor,
                               Ensino = t.Ensino,
                               FuncionarioId = f.Id.ToString(),
                               NomeAno = a.Nome,
                               NomeEscola = e.Nome,
                               NomeProfessor = f.Nome + " " + f.SobreNome,
                               NomeSala = d.Nome,
                               NomeSerie = s.Nome,
                               Periodo = t.Periodo,
                               QtdAulas1Bimestre = t.QtdAulas1Bimestre,
                               QtdAulas2Bimestre = t.QtdAulas2Bimestre,
                               QtdAulas3Bimestre = t.QtdAulas3Bimestre,
                               QtdAulas4Bimestre = t.QtdAulas4Bimestre,
                               SerieId = s.Id.ToString(),
                               UsuarioId = t.UsuarioId.ToString()
                           }).OrderBy(t => t.NomeSerie).Skip(skip).Take(take).ToList();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}
