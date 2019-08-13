using Domain.TipoOcorrenciaContent.Commands.Outputs;
using Domain.TipoOcorrenciaContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.TipoOcorrenciaContent.Repositories
{
    public interface ITipoOcorrenciaRepositorio : IDisposable
    {
        TipoOcorrencia Salvar(TipoOcorrencia obj);
        void Alterar(TipoOcorrencia obj);
        TipoOcorrencia Existe(Guid id);
        IList<ListarTipoOcorrenciaResults> ListarTodos();
        IList<ListarTipoOcorrenciaResults> ListarTodos(Boolean status,int skip,int take);
        IList<ListarTipoOcorrenicaCmbResults> ListarCmb();
        DetalheTipoOcorrenciaResults Detalhes(Guid tipoOcorrenciaId);
    }
}
