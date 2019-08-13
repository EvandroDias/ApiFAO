using Domain.DisciplinaContent.Commands.Outputs;
using Domain.SerieContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.SerieContent.Repositories
{
    public interface IDisciplinaRepositorio : IDisposable
    {
        Disciplina Salvar(Disciplina obj);
        void Alterar(Disciplina obj);
        Disciplina Existe(Guid id);
        IList<ListarDisciplinaResults> ListarTodos();
        IList<ListarDisciplinaResults> ListarTodos(Boolean status, int skip, int take);
        DetalheDisciplinaResults Detalhes(Guid serieId);
    }
}
