using Domain.RotinaContent.Commands.Outputs;
using Domain.RotinaContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.RotinaContent.Repositories
{
    public interface IRotinaRepositorio : IDisposable
    {
        Rotina Salvar(Rotina obj);
        void Alterar(Rotina obj);
        Rotina Existe(Guid id);
        Rotina Existe(Guid id,Boolean status);
        Rotina Existe(Guid id,Guid usuarioId);
        Rotina JaTemRotina(Guid funcionarioId,DateTime de);

        IList<ListarRotinaResults> ListarTodos(int skip,int take);
        IList<ListarRotinaResults> ListarMinhasRotinas(int skip, int take,Guid usuarioId);
        ImprimirRotinaResults Imprimir(Guid id);

        DetalheRotinaResults Detalhes(Guid ocorrenciaId);
    }
}
