using Domain.AlunoConselhoContent.Commands.Outputs;
using Domain.AlunoConselhoContent.Entities;
using Domain.AlunoConselhoContent.Repositories;
using Domain.ConselhoContent.Commands.Outputs;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class AlunoConselhoRepositorio : IAlunoConselhoRepositorio
    {
        private readonly DataContext _context;

        public AlunoConselhoRepositorio(DataContext context)
        {
            this._context = context;
        }
        public AlunoConselho Salvar(AlunoConselho obj)
        {
            var retorno = _context.AlunoConselhos.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(AlunoConselho obj)
        {
            _context.Entry<AlunoConselho>(obj).State = EntityState.Modified;
        }

        public AlunoConselho Existe(Guid id)
        {
            return _context.AlunoConselhos.Where(s => s.Id == id).FirstOrDefault();
        }

        public AlunoConselho AlunoConselhoJaExiste(Guid alunoId, Guid conselhoId)
        {
            return _context.AlunoConselhos.Where(c => c.AlunoId == alunoId && c.ConselhoId == conselhoId).FirstOrDefault();
        }

        public IList<ListarAlunoConselhoResults> ListarTodos(Guid conselhoId)
        {
            var retorno = (from c in _context.AlunoConselhos
                           join a in _context.Alunos on c.AlunoId equals a.Id
                           where c.ConselhoId == conselhoId
                           select new ListarAlunoConselhoResults()
                           {
                               AlunoConselhoId = c.Id,
                               AlunoId = c.AlunoId,
                               ConselhoId = c.ConselhoId,
                               Descricao = c.Descricao,
                               NomeAluno = a.Nome+" "+a.SobreNome,
                               UsuarioId = c.UsuarioId
                           }
                           ).OrderBy(c => c.NomeAluno).ToList();

            return retorno;
        }

        public DetalheAlunoConselhoResults Detalhes(Guid alunoConselhoId)
        {
            var retorno = (from c in _context.AlunoConselhos
                           join a in _context.Alunos on c.AlunoId equals a.Id
                           where c.Id == alunoConselhoId
                           select new DetalheAlunoConselhoResults()
                           {
                               AlunoConselhoId = c.Id,
                               AlunoId = c.AlunoId,
                               ConselhoId = c.ConselhoId,
                               Descricao = c.Descricao,
                               NomeAluno = a.Nome,
                               UsuarioId = c.UsuarioId
                           }
                          ).FirstOrDefault();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
