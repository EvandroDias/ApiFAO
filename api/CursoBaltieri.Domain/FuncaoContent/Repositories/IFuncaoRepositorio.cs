using Domain.FuncaoContent.Commands.Outputs;
using Domain.FuncaoContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.FuncaoContent.Repositories
{
    public interface IFuncaoRepositorio : IDisposable
    {
        Funcao Salvar(Funcao obj);
        void Alterar(Funcao obj);
        Funcao Existe(Guid id);
        IList<ListarFuncaoResults> ListarTodos(Boolean status,int skip,int take);
        IList<ListarFuncaoResults> Listar(Boolean status);
        DetalheFuncaoResults Detalhes(Guid serieId);
    }
}
