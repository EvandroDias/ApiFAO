using Domain.TurmaContent.Commands.Inputs;
using Domain.TurmaContent.Entities;
using Domain.TurmaContent.Repositories;
using Domain.FuncionarioContent.Commands.Inputs;
using Flunt.Notifications;
using Shared.Comands;
using System.Collections.Generic;
using System;

namespace Domain.TurmaContent.Handlers
{
    public class TurmaHandler : Notifiable,
        IComandHandler<SalvarTurmaCommands>,
        IComandHandler<AlterarTurmaCommands>,
        IComandHandler<AtivarDesativarCommands>
    {
        private readonly ITurmaRepositorio _repository;
       

        public TurmaHandler(ITurmaRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(AlterarTurmaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var turma = _repository.Existe(Guid.Parse(comand.TurmaId));

            if(turma != null)
            {
                turma.Alterar(comand.Ensino, comand.SerieId, comand.DepartamentoId, comand.AnoId, comand.Periodo, comand.Coordenador, comand.Diretor, comand.FuncionarioId, comand.QtdAulas1Bimestre, comand.QtdAulas2Bimestre, comand.QtdAulas3Bimestre, comand.QtdAulas4Bimestre);
                _repository.Alterar(turma);
            }
            else
            {
                return new ComandResult(false, "Série não existe,tente novamente!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(SalvarTurmaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var existe = _repository.Existe(Guid.Parse(comand.FuncionarioId),Guid.Parse(comand.AnoId));

            if(existe == null)
            {
                var turma = new Turma(comand.EscolaId, comand.Ensino, comand.UsuarioId, comand.SerieId, comand.DepartamentoId, comand.AnoId, comand.Periodo, comand.NomeCoordenador, comand.NomeDiretor, comand.FuncionarioId, comand.QtdAulas1Bimestre, comand.QtdAulas2Bimestre, comand.QtdAulas3Bimestre, comand.QtdAulas4Bimestre);
                _repository.Salvar(turma);

            }
            else
            {
                 AddNotification("Turma", "Turma já esta cadastrado");
            }

            if (Invalid)
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", Notifications);
            }

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

            var turma = _repository.Existe(comand.Id);

            if (turma != null)
            {
                turma.AtivarDesativar();
                this._repository.Alterar(turma);

                if (turma.Status)
                {


                    return new ComandResult(true,"Turma foi ativada!!", new { r });
                }
                else
                {
                    return new ComandResult(true," Turma foi excluída com sucesso!!", new { r });
                }

            }
            else
            {
                return new ComandResult(false, "Turma não encontrada,tente novamente!!", new { r });
            }


        }
    }
}
