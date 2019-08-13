using Domain.ItemDepartamentoEscolaContent.Entities;
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;
using System.Collections.Generic;

namespace Domain.EscolaContent.Commands.Inputs
{
    public class SalvarEscolaCommands : Notifiable, IComand
    {
        public string Nome { get;  set; }
        public Guid UsuarioId { get;  set; }
        public IList<ItemDepartamentoEscola> Item { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNull(Item,"item","A escola tem que ter seus departamentos!!")
                .IsNotNullOrEmpty(UsuarioId.ToString(), "Usuario", "O usuário é obrigatório!")
                .IsNotNullOrEmpty(Nome, "Nome", "O campo nome é obrigatório!")
                .HasMinLen(Nome, 3, "Nome", "O nome deve conter no minimo 3 caracter!")
              
            );

            return Valid;
        }

        public void SetarUsuarioId(Guid id)
        {
            this.UsuarioId = id;
        }
    }
}
