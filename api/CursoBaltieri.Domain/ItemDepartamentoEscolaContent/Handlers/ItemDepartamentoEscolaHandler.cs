using Domain.ItemDepartamentoEscolaContent.Commands.Inputs;
using Domain.ItemDepartamentoEscolaContent.Entities;
using Domain.ItemDepartamentoEscolaContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;

namespace Domain.ItemDepartamentoEscolaContent.Handlers
{
    public class ItemDepartamentoEscolaHandler : Notifiable,
        IComandHandler<SalvarItemDepartamentoEscolaCommands>,
        IComandHandler<AlterarItemDepartamentoEscolaCommands>
    {
        private readonly IItemDepartamentoEscolaRepositorio _repository;
       

        public ItemDepartamentoEscolaHandler(IItemDepartamentoEscolaRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(AlterarItemDepartamentoEscolaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = _repository.Existe(comand.EscolaId,comand.DepartamentoId);

            if(serie != null)
            {
                serie.Alterar(comand.EscolaId,comand.DepartamentoId);
                _repository.Alterar(serie);
            }
            else
            {
                return new ComandResult(false, "O item não existe,tente novamente!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(SalvarItemDepartamentoEscolaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = new ItemDepartamentoEscola(comand.EscolaId, comand.DepartamentoId);
            _repository.Salvar(serie);

            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
        }
    }
}
