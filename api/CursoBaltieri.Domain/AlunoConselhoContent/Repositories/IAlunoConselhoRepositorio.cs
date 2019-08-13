using Domain.AlunoConselhoContent.Commands.Outputs;
using Domain.AlunoConselhoContent.Entities;
using Domain.ConselhoContent.Commands.Outputs;
using System;
using System.Collections.Generic;

namespace Domain.AlunoConselhoContent.Repositories
{
    public interface IAlunoConselhoRepositorio : IDisposable
    {
        AlunoConselho Salvar(AlunoConselho obj);
        void Alterar(AlunoConselho obj);
        AlunoConselho Existe(Guid id);
        AlunoConselho AlunoConselhoJaExiste(Guid alunoId, Guid conselhoId);
        IList<ListarAlunoConselhoResults> ListarTodos(Guid conselhoId);

        DetalheAlunoConselhoResults Detalhes(Guid serieId);
    }
}
