using Domain.ProvidenciaContent.Commands.Inputs;
using Domain.ProvidenciaContent.Entities;
using Domain.ProvidenciaContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;

namespace Domain.ProvidenciaContent.Handlers
{
    public class ProvidenciaHandler : Notifiable,
        IComandHandler<SalvarProvidenciaCommands>,
        IComandHandler<AlterarProvidenciaCommands>
    {
        private readonly IProvidenciaRepositorio _repository;
       

        public ProvidenciaHandler(IProvidenciaRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(AlterarProvidenciaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            //var serie = _repository.Existe(comand.Id);

            //if(serie != null)
            //{
            //    serie.Alterar(comand.Nome);
            //    _repository.Alterar(serie);
            //}
            //else
            //{
            //    return new ComandResult(false, "Série não existe,tente novamente!!", new { });
            //}

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(SalvarProvidenciaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var ocorrencia = new Providencia(comand.Titulo,comand.Descricao,comand.DataProvidencia,comand.FuncionarioId,comand.OcorrenciaId);
            _repository.Salvar(ocorrencia);

            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
        }
    }
}
