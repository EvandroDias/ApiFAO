using Domain.HorarioRotinaContent.Commands.Outputs;
using Domain.HorarioRotinaContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.HorarioRotinaContent.Repositories
{
    public interface IHorarioRotinaRepositorio : IDisposable
    {
        HorarioRotina Salvar(HorarioRotina obj);
        void Alterar(HorarioRotina obj);
        HorarioRotina Existe(Guid horarioRotinaId);
        HorarioRotina Existe(string aula, Guid rotinaId,Guid diaSemanaId);
        IList<ListarHorarioRotinaResults> ListarTodos();
        IList<ListarHorarioRotinaResults> ListarMeusHorarios(Guid rotinaId, Guid diaSemanaId);
        DetalheHorarioRotinaResults Detalhes(Guid horarioRotinaId,Guid usuarioId);
        DetalheHorarioRotinaResults Detalhes(Guid horarioRotinaId);
    }
}
