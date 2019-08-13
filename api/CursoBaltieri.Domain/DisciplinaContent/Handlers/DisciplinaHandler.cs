using Domain.DisciplinaContent.Commands.Inputs;
using Domain.FuncionarioContent.Commands.Inputs;
using Domain.SerieContent.Entities;
using Domain.SerieContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;
using System.Collections.Generic;

namespace Domain.DisciplinaContent.Handlers
{
    public class DisciplinaHandler : Notifiable,
        IComandHandler<SalvarDisciplinaCommands>,
        IComandHandler<AlterarDisciplinaCommands>,
        IComandHandler<AtivarDesativarCommands>
    {
        private readonly IDisciplinaRepositorio _repository;
       

        public DisciplinaHandler(IDisciplinaRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(SalvarDisciplinaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var serie = new Disciplina(comand.Nome.ToUpper(), comand.UsuarioId);
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

            var disciplina = _repository.Existe(comand.Id);

            if (disciplina != null)
            {
                disciplina.AtivarDesativar();
                this._repository.Alterar(disciplina);

                if (disciplina.Status)
                {


                    return new ComandResult(true, disciplina.Nome + " foi ativado(a)!!", new { r });
                }
                else
                {
                    return new ComandResult(true, disciplina.Nome + " foi desativado(a)!!", new { r });
                }

            }
            else
            {
                return new ComandResult(false, "Disciplina não encontrado,tente novamente!!", new { r });
            }


        }

        public IComandResult Handle(AlterarDisciplinaCommands comand)
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

       
    }
}
