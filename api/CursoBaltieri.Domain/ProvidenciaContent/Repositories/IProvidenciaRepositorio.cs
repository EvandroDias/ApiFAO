using Domain.ProvidenciaContent.Entities;
using System;

namespace Domain.ProvidenciaContent.Repositories
{
    public interface IProvidenciaRepositorio : IDisposable
    {
        Providencia Salvar(Providencia obj);
        void Alterar(Providencia obj);
        Providencia Existe(Guid id);
        //IList<ListarProvidenciaResults> ListarTodos();
        //DetalheProvidenciaResults Detalhes(Guid serieId);
    }
}
