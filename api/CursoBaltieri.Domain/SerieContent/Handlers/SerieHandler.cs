using Domain.FuncionarioContent.Commands.Inputs;
using Domain.SerieContent.Commands.Inputs;
using Domain.SerieContent.Entities;
using Domain.SerieContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;
using System.Collections.Generic;

namespace Domain.SerieContent.Handlers
{
    public class SerieHandler : Notifiable,
        IComandHandler<SalvarSerieCommands>,
        IComandHandler<AlterarSerieCommands>,
        IComandHandler<AtivarDesativarCommands>
    {
        private readonly ISerieRepositorio _repository;
       

        public SerieHandler(ISerieRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(AlterarSerieCommands comand)
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

        public IComandResult Handle(SalvarSerieCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = new Serie(comand.Nome.ToUpper(),comand.Numero, comand.UsuarioId);
            _repository.Salvar(serie);

            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
        }

        public IComandResult Handle(AtivarDesativarCommands comand)
        {
            var r = new List<string>();
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = _repository.Existe(comand.Id);

            if (serie != null)
            {
                serie.AtivarDesativar();
                this._repository.Alterar(serie);

                if (serie.Status)
                {


                    return new ComandResult(true, serie.Nome + " foi ativado!!", new { r });
                }
                else
                {
                    return new ComandResult(true, serie.Nome + " foi desativado!!", new { r });
                }

            }
            else
            {
                return new ComandResult(false, "Funcionário não encontrado,tente novamente!!", new { r });
            }


        }
    }
}
