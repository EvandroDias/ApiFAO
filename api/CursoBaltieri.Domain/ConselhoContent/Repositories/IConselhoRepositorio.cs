using Domain.ConselhoContent.Commands.Inputs;
using Domain.ConselhoContent.Commands.Outputs;
using Domain.ConselhoContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.ConselhoContent.Repositories
{
    public interface IConselhoRepositorio : IDisposable
    {
        Conselho Salvar(Conselho obj);
        void Alterar(Conselho obj);
        Conselho Existe(Guid id);

        Conselho Existe(Guid bimestreId,Guid serieId,Guid anoId);

        IList<ListarConselhoResults> ListarTodos(int skip,int take);
        IList<ListarConselhoResults> ListarPorData(DateTime data);
        IList<ListarConselhoResults> ListarPorPerido(DateTime inicio,DateTime final);
        IList<ListarConselhoResults> ListarPorNomeAluno(string nomeAluno);
        IList<ListarConselhoResults> ListarPorBimestre(Guid bimestreId);
        DetalheConselhoResults Detalhes(Guid ocorrenciaId);
        List<ListarConselhoResults> FiltrarPorSerie(FiltroConselhoCommands command);
    }
}
