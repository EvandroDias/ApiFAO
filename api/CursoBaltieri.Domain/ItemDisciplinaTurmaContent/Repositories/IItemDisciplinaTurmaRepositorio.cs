using Domain.ItemDisciplinaTurmaContent.Entities;
using System;

namespace Domain.DisciplinaTurmaContent.Repositories
{
    public interface IItemDisciplinaTurmaRepositorio : IDisposable
    {
        ItemDisciplinaTurma Salvar(ItemDisciplinaTurma obj);
        void Alterar(ItemDisciplinaTurma obj);
        ItemDisciplinaTurma Existe(Guid disciplinaId, Guid turmaId);
        
    }
}
