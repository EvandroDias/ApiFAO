using Domain.AlunoTurmaContent.Commands.Inputs;
using Domain.AlunoTurmaContent.Repositories;
using Domain.ItemAlunoTurmaContent.Entities;
using Flunt.Notifications;
using Shared.Comands;
using System;

namespace Domain.ItemAlunoTurmaContent.Handlers
{
    public class ItemAlunoTurmaHandler : Notifiable,
        IComandHandler<SalvarItemAlunoTurmaCommands>,
        IComandHandler<AlterarAlunoTurmaCommands>
    {
        private readonly IItemAlunoTurmaRepositorio _repository;
       

        public ItemAlunoTurmaHandler(IItemAlunoTurmaRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(AlterarAlunoTurmaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = _repository.Existe(Guid.Parse(comand.AlunoId), Guid.Parse(comand.TurmaId));

            if(serie != null)
            {
                serie.Alterar(comand.TurmaId,comand.AlunoId,comand.Numero,comand.Status);
                _repository.Alterar(serie);
            }
            else
            {
                return new ComandResult(false, "O item não existe,tente novamente!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(SalvarItemAlunoTurmaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var existe = _repository.Existe(Guid.Parse(comand.AlunoId), Guid.Parse(comand.TurmaId));

            if(existe == null)
            {
                var item = new ItemAlunoTurma(comand.TurmaId, comand.AlunoId, comand.Numero);
                item.SetarStatus(comand.Status);
                _repository.Salvar(item);
            }
            else
            {
                AddNotification("Aluno", "Aluno já esta cadastrado nessa turma");
            }

            if (Invalid)
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", Notifications);
            }

            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
        }
    }
}
