using Domain.AnoContent.Commands.Outputs;
using Domain.AnoContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.AnoContent.Repositories
{
    public interface IAnoRepositorio : IDisposable
    {
        Ano Salvar(Ano obj);
        void Alterar(Ano obj);
        Ano Existe(Guid id);
        Ano Existe(string nome);
        IList<ListarAnoResults> ListarTodos(Boolean status,int skip,int take);
        IList<ListarAnoResults> Listar(Boolean status);
        DetalheAnoResults Detalhes(Guid serieId);
    }
}
