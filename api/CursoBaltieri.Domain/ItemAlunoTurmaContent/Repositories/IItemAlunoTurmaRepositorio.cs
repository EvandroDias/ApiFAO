using Domain.AlunoTurmaContent.Commands.Inputs;
using Domain.ItemAlunoTurmaContent.Entities;
using System;

namespace Domain.AlunoTurmaContent.Repositories
{
    public interface IItemAlunoTurmaRepositorio : IDisposable
    {
        ItemAlunoTurma Salvar(ItemAlunoTurma obj);
        void Alterar(ItemAlunoTurma obj);
        ItemAlunoTurma Existe(Guid alunoId,Guid turmaId);

        DetalheAlunoTurmaResults Detalhe(Guid alunoId, Guid turmaId);

    }
}
