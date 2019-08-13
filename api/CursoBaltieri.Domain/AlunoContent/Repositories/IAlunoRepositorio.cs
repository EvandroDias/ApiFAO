using Domain.AlunoContent.Commands.Outputs;
using Domain.AlunoContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.AlunoContent.Repositories
{
    public interface IAlunoRepositorio : IDisposable
    {
        Aluno Salvar(Aluno obj);
        void Alterar(Aluno obj);
        Aluno Existe(Guid id);
        Aluno AlunoJaExiste(string ra, string rm);
        Aluno AlunoJaExiste(string rm);
        IList<ListarAlunoResults> ListarTodos(Boolean status,int skip,int take);
        IList<ListarAlunoResults> Pesquisar(string nome);
        IList<ListarAlunoCmbResults> ListarCmb();
        IList<ListarAlunoCmbResults> ListarCmb(Guid serieId);
        IList<ListarAlunoCmbResults> ListarPorTurmaId(Guid turmaId);
        DetalheAlunoResults Detalhes(Guid serieId);
    }
}
