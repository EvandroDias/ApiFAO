using Domain.FuncionarioContent.Commands.Inputs;
using Domain.TipoOcorrenciaContent.Commands.Inputs;
using Domain.TipoOcorrenciaContent.Entities;
using Domain.TipoOcorrenciaContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;
using System.Collections.Generic;

namespace Domain.TipoOcorrenciaContent.Handlers
{
    public class TipoOcorrenciaHandler : Notifiable,
        IComandHandler<SalvarTipoOcorrenciaCommands>,
        IComandHandler<AlterarTipoOcorrenciaCommands>,
        IComandHandler<AtivarDesativarCommands>
    {
        private readonly ITipoOcorrenciaRepositorio _repository;
       

        public TipoOcorrenciaHandler(ITipoOcorrenciaRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(AlterarTipoOcorrenciaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var tipoOcorrencia = _repository.Existe(comand.Id);

            if(tipoOcorrencia != null)
            {
                tipoOcorrencia.Alterar(comand.Nome);
                _repository.Alterar(tipoOcorrencia);
            }
            else
            {
                return new ComandResult(false, "Série não existe,tente novamente!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(AtivarDesativarCommands comand)
        {
            var r = new List<string>();
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var tipoDepartamento = _repository.Existe(comand.Id);

            if (tipoDepartamento != null)
            {
                tipoDepartamento.AtivarDesativar();
                this._repository.Alterar(tipoDepartamento);

                if (tipoDepartamento.Status)
                {


                    return new ComandResult(true, tipoDepartamento.Nome + " foi ativado(a)!!", new { r });
                }
                else
                {
                    return new ComandResult(true, tipoDepartamento.Nome + " foi desativado(a)!!", new { r });
                }

            }
            else
            {
                return new ComandResult(false, "Tipo Departamento não encontrado,tente novamente!!", new { r });
            }


        }

        public IComandResult Handle(SalvarTipoOcorrenciaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var tipoOcorrencia = new TipoOcorrencia(comand.Nome.ToUpper());
            _repository.Salvar(tipoOcorrencia);

            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
        }
    }
}
