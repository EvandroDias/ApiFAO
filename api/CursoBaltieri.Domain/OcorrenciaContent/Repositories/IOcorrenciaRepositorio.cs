using Domain.OcorrenciaContent.Commands.Inputs;
using Domain.OcorrenciaContent.Commands.Outputs;
using Domain.OcorrenciaContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.OcorrenciaContent.Repositories
{
    public interface IOcorrenciaRepositorio : IDisposable
    {
        Ocorrencia Salvar(Ocorrencia obj);
        void Alterar(Ocorrencia obj);
        Ocorrencia Existe(Guid id);

        List<ListarOcorrenciaResults> ListarTodos(int skip,int take);
        List<ListarOcorrenciaResults> ListarPorData(DateTime data);
        List<ListarOcorrenciaResults> ListarPorPerido(DateTime inicio,DateTime final);
        List<ListarOcorrenciaResults> ListarPorNomeAluno(string nomeAluno);
        List<ListarOcorrenciaResults> ListarPorTipoOcorrencia(Guid tipoOcorrencia);

        List<ListarOcorrenciaResults> FiltrarPorSerie(FiltroOcorrenciaCommands command);

        List<ListarOcorrenciaResults> FiltrarPorAluno(FiltroOcorrenciaCommands command);
        List<ListarOcorrenciaResults> FiltrarTodoPorData(FiltroOcorrenciaCommands command);
        GraficoOcorrenciaResults GraficoPorData(FiltroOcorrenciaCommands command);
        PainelGraficoOcorrenciaResults GraficoPainel(FiltroOcorrenciaCommands command);
        PainelGraficoOcorrenciaResults GraficoPorSerie(FiltroOcorrenciaCommands command);

        List<ListarOcorrenciaResults> ListarMinhasOcorrencias(int skip, int take,Guid usuarioId);

        DetalheOcorrenciaResults Detalhes(Guid ocorrenciaId);

        List<ListarOcorrenciaResults> Filtrar(FiltroOcorrenciaCommands command);

        List<RetornoTotalOcorrenciaResults> RetornoTotalOcorrencia(FiltroOcorrenciaCommands command);
    }
}
