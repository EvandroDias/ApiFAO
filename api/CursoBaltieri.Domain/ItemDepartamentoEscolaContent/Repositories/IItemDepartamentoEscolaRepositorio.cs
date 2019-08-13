using Domain.ItemDepartamentoEscolaContent.Commands.Inputs;
using Domain.ItemDepartamentoEscolaContent.Entities;
using System;

namespace Domain.ItemDepartamentoEscolaContent.Repositories
{
    public interface IItemDepartamentoEscolaRepositorio : IDisposable
    {
        ItemDepartamentoEscola Salvar(ItemDepartamentoEscola obj);
        void Alterar(ItemDepartamentoEscola obj);
        ItemDepartamentoEscola Existe(Guid escolaId,Guid departamentoId);
        
    }
}
