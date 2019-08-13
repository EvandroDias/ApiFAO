using Domain.FuncionarioContent.Commands.Inputs;
using Domain.FuncionarioContent.Repositories;
using Domain.OcorrenciaContent.Commands.Inputs;
using Domain.OcorrenciaContent.Commands.Outputs;
using Domain.OcorrenciaContent.Entities;
using Domain.OcorrenciaContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;
using System.Collections.Generic;

namespace Domain.OcorrenciaContent.Handlers
{
    public class OcorrenciaHandler : Notifiable,
        IComandHandler<SalvarOcorrenciaCommands>,
        IComandHandler<AlterarOcorrenciaCommands>,
        IComandHandler<FiltroOcorrenciaCommands>,
        IComandHandler<AtivarDesativarCommands>
    {
        private readonly IOcorrenciaRepositorio _repository;
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public OcorrenciaHandler(IOcorrenciaRepositorio repository, IFuncionarioRepositorio funcionarioRepositorio)
        {
            this._repository = repository;
            this._funcionarioRepositorio = funcionarioRepositorio;
        }

        public IComandResult Handle(AlterarOcorrenciaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var ocorrencia = _repository.Existe(comand.OcorrenciaId);

            if (ocorrencia != null)
            {
                ocorrencia.Alterar(comand.Titulo,comand.Descricao,comand.DataOcorrencia,comand.AlunoId,comand.Periodo,comand.SerieId,comand.TipoOcorrenciaId,comand.FuncionarioId);
                _repository.Alterar(ocorrencia);
            }
            else
            {
                return new ComandResult(false, "Ocorrencia não existe,tente novamente!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(SalvarOcorrenciaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

           
                var ocorrencia = new Ocorrencia(comand.Titulo, comand.Descricao, comand.DataOcorrencia, comand.UsuarioId, comand.AlunoId,comand.Periodo,comand.SerieId,comand.TipoOcorrenciaId,comand.FuncionarioId);
                _repository.Salvar(ocorrencia);

                return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
           
        }

        public IComandResult Handle(FiltroOcorrenciaCommands comand)
        {
            var ocorrencia = new List<ListarOcorrenciaResults>();

            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            switch (comand.TipoFiltro)
            {
                case "serie":
                     ocorrencia = _repository.FiltrarPorSerie(comand);
                    return new ComandResult(true, "Ocorrencias!!", new { ocorrencia });

                case "aluno":
                     ocorrencia = _repository.FiltrarPorAluno(comand);
                    return new ComandResult(true, "Ocorrencias!!", new { ocorrencia });

                default:
                    return new ComandResult(true, "Ocorrencias!!", new { ocorrencia });
            }
           

            
        }

        public IComandResult Handle(AtivarDesativarCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var ocorrencia = _repository.Existe(comand.Id);

            if (ocorrencia != null)
            {
                ocorrencia.AtivarDesativar();
                this._repository.Alterar(ocorrencia);

                if (ocorrencia.Status)
                {
                    return new ComandResult(true,"Ocorrência foi ativada!!", new { });
                }
                else
                {
                    return new ComandResult(true,"Ocorrência excluída com sucesso!!", new { });
                }

            }
            else
            {
                return new ComandResult(false, "Ocorrência não encontrada,tente novamente!!", new { });
            }


        }
    }
}
