using Domain.AlunoContent.Commands.Inputs;
using Domain.AlunoContent.Entities;
using Domain.AlunoContent.Repositories;
using Domain.DadoPessoalContent.Entities;
using Domain.DadoPessoalContent.Repositories;
using Domain.FuncionarioContent.Commands.Inputs;
using Flunt.Notifications;
using Shared.Comands;
using System;

namespace Domain.AlunoContent.Handlers
{
    public class AlunoHandler : Notifiable,
        IComandHandler<SalvarAlunoCommands>,
        IComandHandler<AlterarAlunoCommands>,
        IComandHandler<AtivarDesativarCommands>
    {
        private readonly IAlunoRepositorio _repository;
        private readonly IDadoPessoalRepositorio _dadoPessoalRepositorio;

        public AlunoHandler(IAlunoRepositorio repository, IDadoPessoalRepositorio dadoPessoalRepositorio)
        {
            this._repository = repository;
            this._dadoPessoalRepositorio = dadoPessoalRepositorio;
        }

        public IComandResult Handle(SalvarAlunoCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var alunoJaexiste = _repository.AlunoJaExiste(comand.Ra, comand.Rm);

            if (alunoJaexiste != null)
            {
                return new ComandResult(false, "Aluno já está cadastrado!!", new { });
            }

            var dadoPessoal = new DadoPessoal(comand.Rua, comand.Numero, comand.Bairro, comand.Uf, comand.Cidade, comand.Cep, comand.Complemento);

            var ret = _dadoPessoalRepositorio.Salvar(dadoPessoal);

            var aluno = new Aluno(comand.Nome,comand.SobreNome,comand.DataNascimento,comand.Sexo,comand.Nacionalidade,comand.Natural,ret.Id, comand.Ra,comand.Rm,comand.RacaCor,comand.Gemeos,comand.UsuarioId);
            
            _repository.Salvar(aluno);

            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
        }

        public IComandResult Handle(AlterarAlunoCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

           

            var aluno = _repository.Existe(Guid.Parse(comand.AlunoId));

            if(aluno != null)
            {
                var dados = _dadoPessoalRepositorio.BuscarPorId(comand.DadoPessoalId);

                dados.Alterar(comand.Rua, comand.Numero, comand.Bairro, comand.Uf, comand.Cidade, comand.Cep, comand.Complemento);
                aluno.AlterarAluno(comand.Nome,comand.SobreNome,comand.DataNascimento,comand.Sexo,comand.Nacionalidade,comand.Natural,comand.Rm,comand.Ra,comand.Gemeos,comand.RacaCor);
                _repository.Alterar(aluno);
                _dadoPessoalRepositorio.Alterar(dados);
            }
            else
            {
                return new ComandResult(false, "Aluno não encontrado!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(AtivarDesativarCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var aluno = _repository.Existe(comand.Id);

            if (aluno != null)
            {
                aluno.AtivarDesativar();
                this._repository.Alterar(aluno);

                if (aluno.Status)
                {
                    return new ComandResult(true, aluno.Nome + " foi ativado!!", new { });
                }
                else
                {
                    return new ComandResult(true, aluno.Nome + " foi desativado!!", new { });
                }

            }
            else
            {
                return new ComandResult(false, "Aluno não encontrado,tente novamente!!", new { });
            }


        }
    }
}
