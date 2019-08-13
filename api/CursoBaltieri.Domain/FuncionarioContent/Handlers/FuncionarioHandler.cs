using Domain.DadoPessoalContent.Entities;
using Domain.DadoPessoalContent.Repositories;
using Domain.FuncionarioContent.Commands.Inputs;
using Domain.FuncionarioContent.Entities;
using Domain.FuncionarioContent.Repositories;
using Domain.UserContent.Entities;
using Domain.UserContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;
using Shared.Security;
using System;
using System.Collections.Generic;

namespace Domain.FuncionarioContent.Handlers
{
    public class FuncionarioHandler : Notifiable,
        IComandHandler<SalvarFuncionarioCommands>,
        IComandHandler<AlterarFuncionarioCommands>,
        IComandHandler<AtivarDesativarCommands>
        
    {
        private readonly IFuncionarioRepositorio _repository;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IDadoPessoalRepositorio _dadoPessoalRepositorio;


        public FuncionarioHandler(IUsuarioRepositorio usuarioRepositorio,IFuncionarioRepositorio repository,IDadoPessoalRepositorio dadoPessoalRepositorio)
        {
            this._repository = repository;
            this._dadoPessoalRepositorio = dadoPessoalRepositorio;
            this._usuarioRepositorio = usuarioRepositorio;
        }

        public IComandResult Handle(SalvarFuncionarioCommands comand)
        {
            var r = new List<string>();
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }
            var existeCpf = _usuarioRepositorio.ExisteCpf(comand.Cpf);
            var existeEmail = _usuarioRepositorio.ExisteEmail(comand.Email);

            if(existeCpf)
            {
                return new ComandResult(false, "Cpf já está cadastrado,verifique se o usuário não está inativo!", new { });
            }

            if (existeEmail)
            {
                return new ComandResult(false, "Email já está cadastrado,cadastre outro!", new { });
            }
            comand.Senha = "123456";
            var usuario = new Usuario(comand.Nome, comand.SobreNome, comand.Cpf, comand.Senha,comand.Email);
            var chaveSenha = Functions.GetRandomString();
            var _senha = Crypto.EncriptarSenha(comand.Senha, chaveSenha);
            usuario.SetarChaveSenha(chaveSenha, _senha);
            usuario.SetarTipoUsuario(comand.TipoUsuario);
            //usuario.SetarFoto();

            var _user = _usuarioRepositorio.Salvar(usuario);

            var dadoPessoal = new DadoPessoal(comand.Rua, comand.Numero, comand.Bairro, comand.Uf, comand.Cidade, comand.Cep, comand.Complemento);
            var ret = _dadoPessoalRepositorio.Salvar(dadoPessoal);

           

            var funcionario = new Funcionario(comand.Nome,comand.SobreNome,comand.DataNascimento,comand.Sexo,comand.Nacionalidade,comand.Natural,ret.Id, _user.Id,comand.UsuarioId,Guid.Parse(comand.FuncaoId));
            funcionario.SetarCelular(comand.Celular);
            funcionario.SetarEmail(comand.Email);
            funcionario.SetarRgCpf(comand.Rg, comand.Cpf);
            funcionario.SetarTelefoneFixo(comand.TelefoneFixo);
            funcionario.AtivarDesativar();
            var retorno = _repository.Salvar(funcionario);
                    

            return new ComandResult(true, "Dados Salvos com Sucesso!!", new {r });
        }

        public IComandResult Handle(AlterarFuncionarioCommands comand)
        {
            var _r = new List<string>();
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var funcionario = _repository.Existe(comand.Id);

            if(funcionario != null)
            {
                funcionario.Alterar(comand.Nome.ToUpper(),comand.SobreNome.ToUpper(),comand.DataNascimento,comand.Sexo,comand.Nacionalidade,comand.Natural);
                funcionario.SetarCelular(comand.Celular);
                funcionario.SetarEmail(comand.Email);
                funcionario.SetarRgCpf(comand.Rg, comand.Cpf);
                funcionario.SetarTelefoneFixo(comand.TelefoneFixo);
               
                
                var r = _repository.Alterar(funcionario);

                var dadoPessoal = _dadoPessoalRepositorio.BuscarPorId(comand.DadoPessoalId);

                if(dadoPessoal != null)
                {
                    dadoPessoal.Alterar(comand.Rua, comand.Numero, comand.Bairro, comand.Uf, comand.Cidade, comand.Cep, comand.Complemento);
                    _dadoPessoalRepositorio.Alterar(dadoPessoal);
                }

                var _usuario = _usuarioRepositorio.Existe(funcionario.UsuarioId);

                if(_usuario != null)
                {
                     _usuario.SetarTipoUsuario(comand.TipoUsuario);
                    _usuarioRepositorio.Alterar(_usuario);
                }
                
            }
            else
            {
                return new ComandResult(false, "Funcionário não existe,tente novamente!!", new {_r});
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new {_r});
        }

        public IComandResult Handle(AtivarDesativarCommands comand)
        {
            var r = new List<string>();
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var funcionario = _repository.Existe(comand.Id);

            if (funcionario != null)
            {
                funcionario.AtivarDesativar();
                this._repository.Alterar(funcionario);

                if (funcionario.Status)
                {
                    

                    return new ComandResult(true, funcionario.Nome + " foi ativado!!", new {r});
                }
                else
                {
                    return new ComandResult(true, funcionario.Nome + " foi desativado!!", new {r});
                }

            }
            else
            {
                return new ComandResult(false,"Funcionário não encontrado,tente novamente!!", new {r});
            }

            
        }
    }
}
