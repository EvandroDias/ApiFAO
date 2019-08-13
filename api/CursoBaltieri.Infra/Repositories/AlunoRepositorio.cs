using Domain.AlunoContent.Commands.Outputs;
using Domain.AlunoContent.Entities;
using Domain.AlunoContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class AlunoRepositorio : IAlunoRepositorio
    {
        private readonly DataContext _context;

        public AlunoRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Aluno Salvar(Aluno obj)
        {
            var retorno = _context.Alunos.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Aluno obj)
        {
            _context.Entry<Aluno>(obj).State = EntityState.Modified;
        }

        public Aluno Existe(Guid id)
        {
            return _context.Alunos.Where(s => s.Id == id).FirstOrDefault();
        }

        public Aluno AlunoJaExiste(string ra, string rm)
        {
            return _context.Alunos.Where(s => s.Ra == ra && s.Rm == rm && s.Status == true).FirstOrDefault();
        }
        public Aluno AlunoJaExiste(string rm)
        {
            return _context.Alunos.Where(s => s.Rm == rm && s.Status == true).FirstOrDefault();
        }

        public DetalheAlunoResults Detalhes(Guid serieId)
        {
            var retorno = (from s in _context.Alunos
                           join d in  _context.DadoPessoals on s.DadoPessoalId equals d.Id
                           where s.Id == serieId
                           select new DetalheAlunoResults()
                           {
                               Nome = s.Nome,
                               UsuarioId = s.UsuarioId,
                               AlunoId = s.Id,
                               SobreNome = s.SobreNome,
                               Bairro = d.Bairro,
                               Cep = d.Cep,
                               Cidade = d.Cidade,
                               Complemento = d.Complemento,
                               DadoPessoalId = d.Id,
                               DataNascimento = s.DataNascimento,
                               Email = s.Email,
                               Foto = s.Foto,
                               Gemeos = s.Gemeos,
                               Nacionalidade = s.Nacionalidade,
                               Natural = s.Natural,
                               Numero = d.Numero,
                               Ra = s.Ra,
                               Rm = s.Rm,
                               RacaCor = s.RacaCor,
                               Rua = d.Rua,
                               Sexo = s.Sexo,
                               Uf = d.Uf
                               
                           }).FirstOrDefault();

            return retorno;
        }

        public IList<ListarAlunoResults> ListarTodos(Boolean status,int skip,int take)
        {
            var retorno = (from s in _context.Alunos
                           where s.Status == status
                           select new ListarAlunoResults()
                           {
                               Nome = s.Nome,
                               SobreNome = s.SobreNome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderBy(s => s.Nome).Skip(skip).Take(take).ToList();

            return retorno;
        }
        public IList<ListarAlunoResults> Pesquisar(string nome)
        {
            var retorno = (from s in _context.Alunos
                           where s.Status == true && s.Nome.Contains(nome)
                           select new ListarAlunoResults()
                           {
                               Nome = s.Nome,
                               SobreNome = s.SobreNome,
                               UsuarioId = s.UsuarioId,
                               Id = s.Id,
                               Status = s.Status
                           }).OrderBy(s => s.Nome).ToList();

            return retorno;
        }
        public IList<ListarAlunoCmbResults> ListarPorTurmaId(Guid turmaId)
        {
            var retorno = (from t in _context.Turmas
                           join i in _context.ItemAlunoTurmas on t.Id equals i.TurmaId
                           join a in _context.Alunos on i.AlunoId equals a.Id
                           where a.Status == true && t.Id == turmaId
                           select new ListarAlunoCmbResults()
                           {
                               Nome = a.Nome,
                               SobreNome = a.SobreNome,
                               Id = a.Id,
                               Numero = i.Numero,
                               Status = i.Status

                           }).OrderBy(s => s.Nome).ToList();

            return retorno;
        }
        public IList<ListarAlunoCmbResults> ListarCmb(Guid serieId)
        {
            var retorno = (from t in _context.Turmas
                           join i in _context.ItemAlunoTurmas on t.Id equals i.TurmaId
                           join a in _context.Alunos on i.AlunoId equals a.Id
                           where a.Status == true && t.SerieId == serieId
                           select new ListarAlunoCmbResults()
                           {
                               Nome = a.Nome,
                               SobreNome = a.SobreNome,
                               Id = a.Id,
                               Numero = i.Numero,
                               Status = i.Status

                           }).OrderBy(s => s.Nome).ToList();

            return retorno;
        }

        public IList<ListarAlunoCmbResults> ListarCmb()
        {
            var retorno = (from s in _context.Alunos
                           where s.Status == true
                           select new ListarAlunoCmbResults()
                           {
                               Nome = s.Nome,
                               SobreNome = s.SobreNome,
                               Id = s.Id
                           }).OrderBy(s => s.Nome).ToList();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}
