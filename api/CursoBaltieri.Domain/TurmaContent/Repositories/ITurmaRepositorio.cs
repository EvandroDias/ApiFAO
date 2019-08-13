using Domain.TurmaContent.Commands.Outputs;
using Domain.TurmaContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.TurmaContent.Repositories
{
    public interface ITurmaRepositorio : IDisposable
    {
        Turma Salvar(Turma obj);
        void Alterar(Turma obj);
        Turma Existe(Guid turmaId);
        Turma Existe(Guid professorId,Guid anoId);
        Turma ExistePorSerie(Guid serieId);
        IList<ListarTurmaResults> ListarTodos(Boolean status,int skip,int take,Guid anoId);
        IList<ListarTurmaResults> Listar(Boolean status);
        IList<ListarTurmaResults> ListarPorId(Guid turmaId, Guid anoId);
        DetalheTurmaResults Detalhes(Guid serieId);
    }
}
