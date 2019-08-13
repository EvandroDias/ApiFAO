using Domain.HorarioRotinaContent.Commands.Inputs;
using Domain.HorarioRotinaContent.Entities;
using Domain.HorarioRotinaContent.Repositories;
using Domain.RotinaContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;

namespace Domain.HorarioRotinaContent.Handlers
{
    public class HorarioRotinaHandler : Notifiable,
        IComandHandler<SalvarHorarioRotinaCommands>,
        IComandHandler<AlterarHorarioRotinaCommands>
    {
        private readonly IHorarioRotinaRepositorio _repository;
        private readonly IRotinaRepositorio _rotinaRepository;

        public HorarioRotinaHandler(IHorarioRotinaRepositorio repository,IRotinaRepositorio rotinaRepository)
        {
            this._repository = repository;
            this._rotinaRepository = rotinaRepository;
        }

        public IComandResult Handle(AlterarHorarioRotinaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

           

            var horarioRotina = _repository.Existe(comand.HorarioRotinaId);

            if(horarioRotina != null)
            {
                var rotina = _rotinaRepository.Existe(horarioRotina.RotinaId, true);

                if (rotina != null)
                {
                    horarioRotina.Alterar(comand.Conteudo, comand.DiaSemanaId);
                    _repository.Alterar(horarioRotina);
                }
                else
                {
                    return new ComandResult(false, "Não existe nenhuma rotina associado a esse horário!!", new { });
                }


            }
            else
            {
                return new ComandResult(false, "HorárioRotina não existe,tente novamente!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(SalvarHorarioRotinaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var rotina = _rotinaRepository.Existe(comand.RotinaId,true);

            if(rotina != null)
            {
                var existe = _repository.Existe(comand.Aula, comand.RotinaId, comand.DiaSemanaId);

                if (existe != null)
                {
                    return new ComandResult(false, "Já existe um conteúdo cadastrado para essa aula,tente cadastrar em outra aula!!", new { });
                }
                else
                {
                    var horarioRotina = new HorarioRotina(comand.Conteudo, comand.Aula, comand.RotinaId, comand.DiaSemanaId);
                    _repository.Salvar(horarioRotina);
                }
            }
            else
            {
                return new ComandResult(false, "Não existe nenhuma rotina associado a esse horário!!", new { });
            }


        
                       
            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
        }
    }
}
