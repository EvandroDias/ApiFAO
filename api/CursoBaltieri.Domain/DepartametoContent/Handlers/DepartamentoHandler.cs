using Domain.DepartamentoContent.Commands.Inputs;
using Domain.DepartamentoContent.Entities;
using Domain.DepartamentoContent.Repositories;
using Domain.FuncionarioContent.Commands.Inputs;
using Flunt.Notifications;
using Shared.Comands;
using System.Collections.Generic;

namespace Domain.DepartamentoContent.Handlers
{
    public class DepartamentoHandler : Notifiable,
        IComandHandler<SalvarDepartamentoCommands>,
        IComandHandler<AlterarDepartamentoCommands>,
        IComandHandler<AtivarDesativarCommands>
    {
        private readonly IDepartamentoRepositorio _repository;
       

        public DepartamentoHandler(IDepartamentoRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(AlterarDepartamentoCommands comand)
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

        public IComandResult Handle(SalvarDepartamentoCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = new Departamento(comand.Nome.ToUpper(), comand.UsuarioId);
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

            var departamento = _repository.Existe(comand.Id);

            if (departamento != null)
            {
                departamento.AtivarDesativar();

                this._repository.Alterar(departamento);

                if (departamento.Status)
                {


                    return new ComandResult(true, departamento.Nome + " foi ativado!!", new { r });
                }
                else
                {
                    return new ComandResult(true, departamento.Nome + " foi desativado!!", new { r });
                }

            }
            else
            {
                return new ComandResult(false, "Departamento não encontrado,tente novamente!!", new { r });
            }


        }
    }
}
