using Domain.DadoPessoalContent.Commands.Inputs;
using Domain.DadoPessoalContent.Entities;
using Domain.DadoPessoalContent.Repositories;
using Domain.UserContent.Repositories;
using Shared.Comands;
using FluentValidator;


namespace Domain.DadoPessoalContent.Handlers
{
    public class DadoPessoalHandler : Notifiable,
          IComandHandler<SalvarDadoPessoalComand>
    {
        private readonly IDadoPessoalRepositorio _repository;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        
        public DadoPessoalHandler(IDadoPessoalRepositorio repository, IUsuarioRepositorio usuarioRepositorio)
        {
            this._repository = repository;
            this._usuarioRepositorio = usuarioRepositorio;
        }

        public IComandResult Handle(SalvarDadoPessoalComand comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            //var retorno = _repository.BuscarPorUsuarioId(comand.UsuarioId);
            //var usuario = _usuarioRepositorio.Existe(comand.UsuarioId);
            //usuario.SetarConfiDadoPessoal();

            ////verifica se o usuário ja existe
            //if (retorno == null)
            //{
              

                var dadoPessoal = new DadoPessoal(comand.Rua, comand.Numero, comand.Bairro, comand.Uf, comand.Cidade, comand.Cep, comand.Complemento);
                _repository.Salvar(dadoPessoal);

               

                return new ComandResult(true, "Cadastro efetuado com sucesso!", new { });
            //}
            //else
            //{
            //    retorno.Alterar(comand.Rua, comand.Numero, comand.Bairro, comand.Uf, comand.Cidade, comand.Cep, comand.Complemento);
            //    _repository.Alterar(retorno);
            //    return new ComandResult(true, "Dados alterado com sucesso!", new { });
            //}

            

        }
    }
}
