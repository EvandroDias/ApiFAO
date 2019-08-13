using Domain.Services;
using Domain.UserContent.Commands.Inputs;
using Domain.UserContent.Entities;
using Domain.UserContent.Repositories;
using Shared.Comands;
using Shared.Security;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.UserContent.Handlers
{
    public class UsuarioHandler :  Notifiable,
        IComandHandler<RegistroUsuarioComand>,
        IComandHandler<AlterarUsuarioComand>,
        IComandHandler<TrocarSenhaUsuarioComand>,
        IComandHandler<AutenticarUsuarioComand>,
        IComandHandler<EmailEsqueceuSenhaComand>,
        IComandHandler<EsqueceuSenhaComand>,
        IComandHandler<AtivarDesativarUsuarioCommand>

    {
        private readonly IUsuarioRepositorio _repository;
        private readonly IEnviarEmailServices _enviarEmailServices;


        public UsuarioHandler(IUsuarioRepositorio repository, IEnviarEmailServices enviarEmailServices) 
        {
            this._repository = repository;
            this._enviarEmailServices = enviarEmailServices;
            
        }
        public IComandResult Handle(RegistroUsuarioComand comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            //verifica se o usuário ja existe
             if (_repository.ExisteEmail(comand.Email))
                AddNotification("Usuario", "E-mail já esta cadastrado");

            // Criar os VOs

            //var email = new EmailObjectShared(comand.Address);

            // Criar a entidade

            var usuario = new Usuario(comand.Nome,comand.SobreNome,comand.Email,comand.Senha,comand.Email);
            var chaveSenha = Functions.GetRandomString();
            var _senha = Crypto.EncriptarSenha(comand.Senha,chaveSenha);
            usuario.SetarChaveSenha(chaveSenha,_senha);
            usuario.SetarFoto();
            // Validar entidades e VOs

            //AddNotifications(email.Notifications);

            if (Invalid)
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", Notifications);
            }

            // Persistir no banco

            _repository.Salvar(usuario);
            

            // Enviar um E-mail de boas vindas

            // Retornar o resultado para tela

            return new ComandResult(true, "Cadastro efetuado com sucesso", new { Id = usuario.Id,Email = usuario.Login });
        }

        public IComandResult Handle(AlterarUsuarioComand comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            //verifica se o usuário ja existe
             var usuario = _repository.Existe(comand.UsuarioId);

            if(usuario != null)
            {
                usuario.Alterar(comand.Nome, comand.SobreNome,comand.Email);
                _repository.Alterar(usuario);
                
            }
                
       

            if (Invalid)
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", Notifications);
            }


            return new ComandResult(true, "Dado alterado com sucesso", new {});
        }

        public IComandResult Handle(TrocarSenhaUsuarioComand comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var _usuario = _repository.Existe(comand.UsuarioId);

            if(_usuario.Login != comand.Login)
            {
                AddNotification("Usuario", "Email não encontrado,digite o email cadastrado no App");
                //return new ComandResult(false, "Email não encontrado,digite o email cadastrado no App.",new { });
            }

          

            //verifica se o usuário ja existe
            var usuario = _repository.Autenticar(comand.Login,comand.Senha);

            if (usuario == null)
            {
                AddNotification("Usuario", "Senha ou email inválidos!");
                //return new ComandResult(false, "Senha ou email inválidos!", Notifications);
            }
            else
            {
                
                var chaveSenha = Functions.GetRandomString();
                var _senha = Crypto.EncriptarSenha(comand.SenhaNova, chaveSenha);
                usuario.TrocarSenha(_senha, chaveSenha);

                _repository.Alterar(usuario);
            }


            if (Invalid)
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", Notifications);
            }



            return new ComandResult(true, "Senha alterada com sucesso", new {usuario.Id,usuario.Login,usuario.Nome,usuario.SobreNome,usuario.Foto,usuario.TipoUsuario });
        }

        public IComandResult Handle(AutenticarUsuarioComand comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            //verifica se o usuário ja existe
            var usuario = _repository.Autenticar(comand.Login, comand.Senha);

            if (usuario == null)
            {
                AddNotification("Usuario", "Senha ou email inválidos!");
            }
            else
            {
                usuario.SetarUltimoAcesso();
                _repository.Alterar(usuario);
            }
           
            //AddNotifications(email.Notifications);
            if (Invalid)
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", Notifications);
            }

            // Persistir no banco

           
            // Enviar um E-mail de boas vindas

            // Retornar o resultado para tela

            return new ComandResult(true, "Login efetuado com sucesso", new { usuario.Login, usuario.Id,usuario.Nome,usuario.SobreNome,usuario.TipoUsuario,usuario.Foto,usuario.ConfigDadoPessoal});
        }

        public IComandResult Handle(EmailEsqueceuSenhaComand comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            //verifica se o usuário ja existe
            var usuario = _repository.BuscarPorEmail(comand.Email);

            if (usuario == null)
            {
                AddNotification("Usuario", "Email não cadastrado!");
            }
            else
            {
               usuario.GerarRecuperSenha();
                _repository.Alterar(usuario);
            }
                        

            if (Invalid)
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", Notifications);
            }

            var resultato = new ComandResult(true, "Um e-mail foi enviado para o e-email "+ usuario.Login, new { usuario.Id, usuario.Login });

            if (resultato.Success)
            {
                var enviou =_enviarEmailServices.EnviarCodigo("quitosp@hotmail.com", new List<string>() { usuario.Login }, "quitosp@hotmail.com", null, true, "teste", "<html><body><a href='http://localhost:4200/recuperar-senha/" + usuario.RecuperarSenha + "'><button>Texto do botão</button></a></body></html>", "smtp.gmail.com");
                if (enviou)
                    return resultato;
            }
            else
            {
                AddNotification("Erro", "Email não enviado!");
            }

            return new ComandResult(false, "Falha em enviar o e-email,tente novamente!", Notifications);
        }

        public IComandResult Handle(EsqueceuSenhaComand comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            //verifica se o código ja existe
            var retorno = _repository.VerificarCodigoRecuperarSenha(comand.RecuperarSenha.ToString());

            if(retorno == null)
            {
                AddNotification("Código", "Código inválido!");
            }
            else
            {
                var data = DateTime.Now;
                TimeSpan t = data.Subtract(retorno.DataRecuperacao);

                if (t.TotalMinutes < 5)
                {
                    var chaveSenha = Functions.GetRandomString();
                    var _senha = Crypto.EncriptarSenha(comand.Senha, chaveSenha);
                    retorno.TrocarSenha(_senha, chaveSenha);
                }
                else
                {
                    return new ComandResult(false, "Tempo de troca de senha expirou,tenta novamente", Notifications);
                }
            }
        

            if (Invalid)
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", Notifications);
            }

            if (!Invalid)
                _repository.Alterar(retorno);

            var resultato = new ComandResult(true, "Senha alterada com sucesso ", new { });

            if (resultato.Success)
            {
                var enviou = _enviarEmailServices.EnviarCodigo("quitosp@hotmail.com", new List<string>() {retorno.Login }, "quitosp@hotmail.com", null, true, "teste", "<html><body><h1>Sua senha foi alterada com sucesso!!</h1></body></html>", "smtp.gmail.com");
                if (enviou)
                    return resultato;
            }
            else
            {
                AddNotification("Erro", "Erro em alterar a senha,tente novamente!");
            }

            return new ComandResult(false, "Falha em enviar o e-email,tente novamente!", Notifications);


        }

        public IComandResult Handle(AtivarDesativarUsuarioCommand comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var usuario = _repository.ExisteAtivoInativo(comand.Id);

            if(usuario != null)
               {
                  usuario.AtivarDesativar();
                _repository.Alterar(usuario);
            }
            else
            {
                return new ComandResult(false, "Usuário não encontrado", new { });
            }


            if (Invalid)
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", Notifications);
            }

            return new ComandResult(true, "Usuário alterado com sucesso!", new { });
        }
    }
}
