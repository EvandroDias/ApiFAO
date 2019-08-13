using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Api.Model;
using Api.Security;
using Api.Util;
using Domain.UserContent.Commands.Inputs;
using Domain.UserContent.Commands.Outputs;
using Domain.UserContent.Handlers;
using Domain.UserContent.Repositories;
using Shared.Comands;
using Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : TransacaoBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly UsuarioHandler _usuarioHandler;
        private dynamic usuarioComand;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, UsuarioHandler usuarioHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._usuarioRepositorio = usuarioRepositorio;
            this._usuarioHandler = usuarioHandler;
        }

        [AllowAnonymous]
        [HttpPost]
        public object Post(

            [FromBody]AutenticarUsuarioComand usuario,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)

            {

            bool credenciaisValidas = false;

            var resposta = (ComandResult)_usuarioHandler.Handle(usuario);
            

            if (resposta.Success == false)
            {
                return resposta;
            }
            else
            {
                usuarioComand = DeserializerObjeto<UsuarioComand>.RetornoObjetoTipado(resposta.Data); 
                credenciaisValidas = true;
                Commit(resposta.Success);
            }

                //var usuarioBase = usersDAO.Find(usuario.UserID);

                //credenciaisValidas = (usuarioBase != null usuario.UserID == usuarioBase.UserID && usuario.AccessKey == usuarioBase.AccessKey);
                                

            if (credenciaisValidas)
            {
                    
                    ClaimsIdentity identity = new ClaimsIdentity(

                    new GenericIdentity(usuarioComand.Id, "UsuarioId"),

                    new[] {

                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName,usuarioComand.Id),
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Login),
                        new Claim(JwtRegisteredClaimNames.Sid,usuarioComand.Id ),
                        new Claim("Administrador",usuarioComand.TipoUsuario)
                        //new Claim(JwtRegisteredClaimNames.NameId, usuario.Nome),
                        //new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Id.ToString())

                    }

                );

                var usuarioHomeCommandResult = new UsuarioHomeCommandResult(usuario.Login, usuarioComand.Nome,usuarioComand.SobreNome,usuarioComand.Foto,usuarioComand.TipoUsuario);

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +

               TimeSpan.FromSeconds(tokenConfigurations.Seconds);
                 

                var handler = new JwtSecurityTokenHandler();

                var securityToken = handler.CreateToken(new SecurityTokenDescriptor

                {

                    Issuer = tokenConfigurations.Issuer,

                    Audience = tokenConfigurations.Audience,

                    SigningCredentials = signingConfigurations.SigningCredentials,

                    Subject = identity,

                    NotBefore = dataCriacao,

                    Expires = dataExpiracao
                    

                });

                var token = handler.WriteToken(securityToken);


               return new
                {

                    authenticated = true,

                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),

                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),

                    accessToken = token,

                    message = "OK",

                    Usuario = usuarioComand

                };

            }

            else

            {

                return new

                {

                    authenticated = false,

                    message = "Falha ao autenticar"

                };

            }

        }

    }
}