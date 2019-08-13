using Domain.DadoPessoalContent.Entities;
using System;

namespace Domain.DadoPessoalContent.Repositories
{
    public interface IDadoPessoalRepositorio : IDisposable
    {
        bool Existe(Guid Id);
        DadoPessoal BuscarPorId(Guid id);
        //DadoPessoal BuscarPorUsuarioId(Guid usuarioId);
        //DadoPessoalCommandResult ListarPorUsuarioId(Guid usuarioId);
        DadoPessoal Salvar(DadoPessoal dadoPessoal);
        void Alterar(DadoPessoal dadoPessoal);
     }
}
