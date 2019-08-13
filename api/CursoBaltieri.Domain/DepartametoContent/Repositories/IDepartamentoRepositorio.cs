using Domain.DepartamentoContent.Commands.Outputs;
using Domain.DepartamentoContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.DepartamentoContent.Repositories
{
    public interface IDepartamentoRepositorio : IDisposable
    {
        Departamento Salvar(Departamento obj);
        void Alterar(Departamento obj);
        Departamento Existe(Guid id);
        Departamento Existe(string nome);
        IList<ListarDepartamentoResults> ListarTodos(Boolean status,int skip,int take);
        IList<ListarDepartamentoResults> ListarTodos();
        DetalheDepartamentoResults Detalhes(Guid serieId);
    }
}
