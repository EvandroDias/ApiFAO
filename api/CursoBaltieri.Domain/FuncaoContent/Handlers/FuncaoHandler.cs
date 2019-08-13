using Domain.FuncaoContent.Commands.Inputs;
using Domain.FuncaoContent.Entities;
using Domain.FuncaoContent.Repositories;
using Domain.FuncionarioContent.Commands.Inputs;
using Flunt.Notifications;
using Shared.Comands;
using System.Collections.Generic;

namespace Domain.FuncaoContent.Handlers
{
    public class FuncaoHandler : Notifiable,
        IComandHandler<SalvarFuncaoCommands>,
        IComandHandler<AlterarFuncaoCommands>,
        IComandHandler<AtivarDesativarCommands>
    {
        private readonly IFuncaoRepositorio _repository;
       

        public FuncaoHandler(IFuncaoRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(AlterarFuncaoCommands comand)
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

        public IComandResult Handle(SalvarFuncaoCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = new Funcao(comand.Nome.ToUpper(), comand.UsuarioId);
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

            var funcao = _repository.Existe(comand.Id);

            if (funcao != null)
            {
                funcao.AtivarDesativar();
                this._repository.Alterar(funcao);

                if (funcao.Status)
                {


                    return new ComandResult(true, funcao.Nome + " foi ativado!!", new { r });
                }
                else
                {
                    return new ComandResult(true, funcao.Nome + " foi desativado!!", new { r });
                }

            }
            else
            {
                return new ComandResult(false, "Função não encontrado,tente novamente!!", new { r });
            }


        }
    }
}
