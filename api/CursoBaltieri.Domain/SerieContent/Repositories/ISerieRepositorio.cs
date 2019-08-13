using Domain.SerieContent.Commands.Outputs;
using Domain.SerieContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.SerieContent.Repositories
{
    public interface ISerieRepositorio : IDisposable
    {
        Serie Salvar(Serie obj);
        void Alterar(Serie obj);
        Serie Existe(Guid id);
        Serie Existe(string nome);
        IList<ListarSerieResults> Listar(Boolean status);
        IList<ListarSerieResults> ListarTodos(Boolean status, int skip, int take);
        IList<ListarSerieCmbResults> ListarCmb();
        DetalheSerieResults Detalhes(Guid serieId);
    }
}
