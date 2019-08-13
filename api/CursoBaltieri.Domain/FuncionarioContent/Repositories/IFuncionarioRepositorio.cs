using Domain.FuncionarioContent.Commands.Outputs;
using Domain.FuncionarioContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.FuncionarioContent.Repositories
{
    public interface IFuncionarioRepositorio : IDisposable
    {
        Funcionario Salvar(Funcionario obj);
        Funcionario Alterar(Funcionario obj);
        Funcionario Existe(Guid id);
        Funcionario Existe(string cpf);
        Funcionario BuscarPorIdUsuario(Guid id);
        IList<ListarFuncionarioResults> ListarTodos(Boolean status,int skip,int take);
        IList<ListarFuncionarioResults> Pesquisar(string nome);

        IList<ListarFuncionarioCmbResults> ListarCordenador(string nome);

        IList<ListarFuncionarioCmbResults> ListarCmb();
        DetalheFuncionarioResults Detalhes(Guid serieId);
        
    }
}
