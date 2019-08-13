using Domain.EscolaContent.Commands.Inputs;
using Domain.EscolaContent.Entities;
using Domain.EscolaContent.Repositories;
using Domain.ItemDepartamentoEscolaContent.Entities;
using Domain.ItemDepartamentoEscolaContent.Repositories;
using Flunt.Notifications;
using Shared.Comands;

namespace Domain.EscolaContent.Handlers
{
    public class EscolaHandler : Notifiable,
        IComandHandler<SalvarEscolaCommands>,
        IComandHandler<AlterarEscolaCommands>,
        IComandHandler<AdicionarDepartamentoCommands>
    {
        private readonly IEscolaRepositorio _repository;
        private readonly IItemDepartamentoEscolaRepositorio _itemRepositorio;

        public EscolaHandler(IEscolaRepositorio repository,IItemDepartamentoEscolaRepositorio itemRepositorio)
        {
            this._repository = repository;
            this._itemRepositorio = itemRepositorio;
        }

        public IComandResult Handle(SalvarEscolaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var escola = new Escola(comand.Nome.ToUpper(), comand.UsuarioId);
            var retorno =_repository.Salvar(escola);

            foreach (var i in comand.Item)
            {
                var existe = _itemRepositorio.Existe(retorno.Id, i.DepartamentoId);
                if(existe == null)
                {
                    var item = new ItemDepartamentoEscola(retorno.Id, i.DepartamentoId);
                    _itemRepositorio.Salvar(item);
                }
            }

            return new ComandResult(true, "Dados Salvos com Sucesso!!", new { });
        }

        public IComandResult Handle(AlterarEscolaCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var escola = _repository.Existe(comand.Id);

            if(escola != null)
            {
                escola.Alterar(comand.Nome);
                _repository.Alterar(escola);
            }
            else
            {
                return new ComandResult(false, "Série não existe,tente novamente!!", new { });
            }

            return new ComandResult(true, "Dados Alterados com Sucesso!!", new { });
        }

        public IComandResult Handle(AdicionarDepartamentoCommands comand)
        {
            //verificar se tem notificação no comand
            if (!comand.IsValid())
            {
                return new ComandResult(false, "Por favor corrija os campos abaixo", comand.Notifications);
            }

            var escola = _repository.Existe(comand.EscolaId);

            if (escola != null)
            {
                foreach (var i in comand.DepartamentoId)
                {
                    var existe = _itemRepositorio.Existe(escola.Id, i.DepartamentoId);

                    if (existe == null)
                    {
                        var item = new ItemDepartamentoEscola(escola.Id, i.DepartamentoId);
                        _itemRepositorio.Salvar(item);
                    }
                }
            }
            else
            {
                return new ComandResult(false, "Escola não encontrada,tente novamente!!", new { });
            }

            return new ComandResult(true, "Departamento adicionado com sucesso!!", new { });
        }
    }
}
