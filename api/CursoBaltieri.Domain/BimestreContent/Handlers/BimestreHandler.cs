﻿using Domain.BimestreContent.Commands.Inputs;
using Domain.BimestreContent.Entities;
using Domain.BimestreContent.Repositories;
using Domain.FuncionarioContent.Commands.Inputs;
using Flunt.Notifications;
using Shared.Comands;
using System.Collections.Generic;

namespace Domain.BimestreContent.Handlers
{
    public class BimestreHandler : Notifiable,
        IComandHandler<SalvarBimestreCommands>,
        IComandHandler<AlterarBimestreCommands>,
        IComandHandler<AtivarDesativarCommands>
    {
        private readonly IBimestreRepositorio _repository;
       

        public BimestreHandler(IBimestreRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(AlterarBimestreCommands comand)
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

        public IComandResult Handle(SalvarBimestreCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = new Bimestre(comand.Nome.ToUpper(), comand.UsuarioId);
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
