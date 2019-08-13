using Domain.DiaSemanaContent.Commands.Inputs;
using Domain.DiaSemanaContent.Entities;
using Domain.DiaSemanaContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;

namespace Domain.DiaSemanaContent.Handlers
{
    public class DiaSemanaHandler : Notifiable,
        IComandHandler<SalvarDiaSemanaCommands>,
        IComandHandler<AlterarDiaSemanaCommands>
    {
        private readonly IDiaSemanaRepositorio _repository;
       

        public DiaSemanaHandler(IDiaSemanaRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(AlterarDiaSemanaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = _repository.Existe(comand.Id);

            if(serie != null)
            {
                serie.Alterar(comand.Nome);
                _repository.Alterar(serie);
            }
            else
            {
                return new ComandResult(false, "Série não existe,tente novamente!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(SalvarDiaSemanaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = new DiaSemana(comand.Nome.ToUpper());
            _repository.Salvar(serie);

            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
        }
    }
}
