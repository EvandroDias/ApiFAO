using Domain.DiaSemanaContent.Commands.Outputs;
using Domain.DiaSemanaContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.DiaSemanaContent.Repositories
{
    public interface IDiaSemanaRepositorio : IDisposable
    {
        DiaSemana Salvar(DiaSemana obj);
        void Alterar(DiaSemana obj);
        DiaSemana Existe(Guid id);
        IList<ListarDiaSemanaResults> ListarTodos();
        DetalheDiaSemanaResults Detalhes(Guid serieId);
    }
}
