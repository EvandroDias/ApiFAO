using Domain.ConselhoContent.Commands.Inputs;
using Domain.ConselhoContent.Entities;
using Domain.ConselhoContent.Repositories;
using Domain.FuncionarioContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;
using System;

namespace Domain.ConselhoContent.Handlers
{
    public class ConselhoHandler : Notifiable,
        IComandHandler<SalvarConselhoCommands>,
        IComandHandler<AlterarConselhoCommands>
       
    {
        private readonly IConselhoRepositorio _repository;
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public ConselhoHandler(IConselhoRepositorio repository, IFuncionarioRepositorio funcionarioRepositorio)
        {
            this._repository = repository;
            this._funcionarioRepositorio = funcionarioRepositorio;
        }

        public IComandResult Handle(AlterarConselhoCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var conselho = _repository.Existe(comand.ConselhoId);

            if (conselho != null)
            {
                conselho.Alterar(comand.DataConselho,Guid.Parse(comand.SerieId) , Guid.Parse(comand.BimestreId), Guid.Parse(comand.FuncionarioId),comand.NomeCoordenador,comand.NomeDiretor, Guid.Parse(comand.AnoId));
                _repository.Alterar(conselho);
            }
            else
            {
                return new ComandResult(false, "Conselho não existe,tente novamente!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(SalvarConselhoCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var conselho = _repository.Existe(Guid.Parse(comand.BimestreId),Guid.Parse(comand.SerieId),Guid.Parse(comand.AnoId));

            if(conselho != null)
            {
                return new ComandResult(false, "Conselho já existe,tente novamente!!", new { });
            }

            var _conselho = new Conselho(comand.DataConselho, comand.UsuarioId,Guid.Parse(comand.SerieId), Guid.Parse(comand.BimestreId), Guid.Parse(comand.FuncionarioId),comand.NomeCoordenador,comand.NomeDiretor,Guid.Parse(comand.AnoId));
                _repository.Salvar(_conselho);

                return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
           
        }

        
    }
}
