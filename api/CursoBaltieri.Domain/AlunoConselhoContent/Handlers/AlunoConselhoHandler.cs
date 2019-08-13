using Domain.AlunoConselhoContent.Commands.Inputs;
using Domain.AlunoConselhoContent.Entities;
using Domain.AlunoConselhoContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;
using System;

namespace Domain.AlunoConselhoContent.Handlers
{
    public class AlunoConselhoHandler : Notifiable,
        IComandHandler<SalvarAlunoConselhoCommands>,
        IComandHandler<AlterarAlunoConselhoCommands>
        
    {
        private readonly IAlunoConselhoRepositorio _repository;
       

        public AlunoConselhoHandler(IAlunoConselhoRepositorio repository)
        {
            this._repository = repository;
        }

        public IComandResult Handle(SalvarAlunoConselhoCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var alunoJaexiste = _repository.AlunoConselhoJaExiste(Guid.Parse(comand.AlunoId),Guid.Parse(comand.ConselhoId));

            if (alunoJaexiste != null)
            {
                return new ComandResult(false, "Aluno já está cadastrado!!", new { });
            }


            var aluno = new AlunoConselho(comand.UsuarioId, comand.Descricao, Guid.Parse(comand.AlunoId),Guid.Parse(comand.ConselhoId));
            
            _repository.Salvar(aluno);

            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
        }

        public IComandResult Handle(AlterarAlunoConselhoCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var aluno = _repository.Existe(comand.AlunoConselhoId);

            //var aluno = new AlunoConselho(Guid.Parse(comand.UsuarioId), comand.Descricao, Guid.Parse(comand.AlunoId),Guid.Parse(comand.ConselhoId));

            if (aluno != null)
            {

                aluno.AlterarAlunoConselho(comand.Descricao,Guid.Parse(comand.AlunoId),Guid.Parse(comand.ConselhoId));
                _repository.Alterar(aluno);
                
            }
            else
            {
                return new ComandResult(false, "Aluno não encontrado!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

       
    }
}
