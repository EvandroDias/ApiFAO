using Domain.EscolaContent.Commands.Outputs;
using Domain.EscolaContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.EscolaContent.Repositories
{
    public interface IEscolaRepositorio : IDisposable
    {
        Escola Salvar(Escola obj);
        void Alterar(Escola obj);
        Escola Existe(Guid id);
        Escola Existe(string nome);
        IList<ListarEscolaResults> ListarTodos();
        DetalheEscolaResults Detalhes(Guid serieId);
    }
}
