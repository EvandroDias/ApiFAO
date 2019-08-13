using Domain.BimestreContent.Commands.Outputs;
using Domain.BimestreContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.BimestreContent.Repositories
{
    public interface IBimestreRepositorio : IDisposable
    {
        Bimestre Salvar(Bimestre obj);
        void Alterar(Bimestre obj);
        Bimestre Existe(Guid id);
        IList<ListarBimestreResults> ListarTodos(Boolean status,int skip,int take);
        IList<ListarBimestreResults> Listar(Boolean status);
        DetalheBimestreResults Detalhes(Guid serieId);
    }
}
