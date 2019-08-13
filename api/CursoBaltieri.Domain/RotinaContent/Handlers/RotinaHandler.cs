using Domain.FuncionarioContent.Commands.Inputs;
using Domain.FuncionarioContent.Repositories;
using Domain.RotinaContent.Commands.Inputs;
using Domain.RotinaContent.Entities;
using Domain.RotinaContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;
using Shared.Util;
using System;
using System.Globalization;

namespace Domain.RotinaContent.Handlers
{
    public class RotinaHandler : Notifiable,
        IComandHandler<SalvarRotinaCommands>,
        IComandHandler<AlterarRotinaCommands>,
        IComandHandler<AtivarDesativarCommands>
    {
        private readonly IRotinaRepositorio _repository;
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public RotinaHandler(IRotinaRepositorio repository, IFuncionarioRepositorio funcionarioRepositorio)
        {
            this._repository = repository;
            this._funcionarioRepositorio = funcionarioRepositorio;
        }

        public IComandResult Handle(AlterarRotinaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var rotina = _repository.Existe(comand.RotinaId,comand.UsuarioId);

            if (rotina != null)
            {
                rotina.Alterar(comand.De,comand.Ate,comand.SerieId);
                _repository.Alterar(rotina);
            }
            else
            {
                return new ComandResult(false, "Rotina não encontrada!!,tente novamente!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(SalvarRotinaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            //var funcionario = _funcionarioRepositorio.BuscarPorIdUsuario(comand.UsuarioId);

            //if(funcionario != null)
            // {
            //var existe = _repository.JaTemRotina(comand.UsuarioId,comand.De);

                var funcionario = _funcionarioRepositorio.BuscarPorIdUsuario(comand.UsuarioId);

            if (funcionario == null)
            {
                return new ComandResult(false, "Funcionário não encontrado,tente novamente!!", new { });
            }
            else
            {

                var rotina = new Rotina(funcionario.Id, comand.ImgCabecalho, comand.De, comand.Ate, comand.SerieId);
                    _repository.Salvar(rotina);

            }

            //  }
            // else
            //  {
            //      return new ComandResult(false, "Funcionário não encontrado,tente novamente!!", new { });
            //  }



            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });

        }

        public IComandResult Handle(AtivarDesativarCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var rotina = _repository.Existe(comand.Id);

            if (rotina != null)
            {
                rotina.AtivarDesativar();
                this._repository.Alterar(rotina);

                if (rotina.Status)
                {
                    return new ComandResult(true, "Rotina ativada com sucesso !!", new { });
                }
                else
                {
                    return new ComandResult(true, "Rotina excluída com sucesso!!", new { });
                }

            }
            else
            {
                return new ComandResult(false, "Funcionário não encontrado,tente novamente!!", new { });
            }


        }

        protected Boolean isfinalDeSemana(DateTime _data)
        {
            Boolean _retorno = false;

            if (_data.DayOfWeek == DayOfWeek.Saturday)
                _retorno = true;
            else if (_data.DayOfWeek == DayOfWeek.Sunday)
                _retorno = true;

            return _retorno;
        }

        //public IComandResult Handle(FiltroRotinaCommands comand)
        //{
        //    var ocorrencia = new FiltroRotinaResults();
        //    if (!comand.IsValid())
        //    {
        //        return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
        //    }

        //    switch (comand.TipoFiltro)
        //    {
        //        case "serie":
        //             ocorrencia = _repository.FiltrarPorSerie(comand);
        //            return new ComandResult(true, "Rotinas!!", new { ocorrencia });

        //        case "aluno":
        //             ocorrencia = _repository.FiltrarPorAluno(comand);
        //            return new ComandResult(true, "Rotinas!!", new { ocorrencia });

        //        default:
        //            return new ComandResult(true, "Rotinas!!", new { ocorrencia });
        //    }



        //}
    }
}
