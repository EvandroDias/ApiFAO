using Domain.DisciplinaTurmaContent.Commands.Inputs;
using Domain.DisciplinaTurmaContent.Repositories;
using Domain.ItemDisciplinaTurmaContent.Entities;
using Flunt.Notifications;
using Shared.Comands;
using System;

namespace Domain.ItemDisciplinaTurmaContent.Handlers
{
    public class ItemDisciplinaTurmaHandler : Notifiable,
        IComandHandler<SalvarDisciplinaTurmaCommands>,
        IComandHandler<AlterarDisciplinaTurmaCommands>
    {
        private readonly IItemDisciplinaTurmaRepositorio _repository;
       

        public ItemDisciplinaTurmaHandler(IItemDisciplinaTurmaRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(AlterarDisciplinaTurmaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = _repository.Existe(Guid.Parse(comand.DisciplinaId), Guid.Parse(comand.TurmaId));

            if(serie != null)
            {
                serie.Alterar(comand.TurmaId,comand.DisciplinaId);
                _repository.Alterar(serie);
            }
            else
            {
                return new ComandResult(false, "O item não existe,tente novamente!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(SalvarDisciplinaTurmaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var item = new ItemDisciplinaTurma(comand.TurmaId, comand.DisciplinaId);
            _repository.Salvar(item);

            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
        }
    }
}
