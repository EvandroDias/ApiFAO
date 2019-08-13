﻿using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;
using System;

namespace Domain.AnoContent.Commands.Inputs
{
    public class AlterarAnoCommands : Notifiable, IComand
    {
        public string Nome { get; set; }
        public Guid Id { get; set; }

        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Id.ToString(), "Ano", "O campo id é obrigatório!")
                .IsNotNullOrEmpty(Nome, "Nome", "O campo nome é obrigatório!")
                .HasMinLen(Nome, 3, "Nome", "O nome deve conter no minimo 3 caracter!")

            );

            return Valid;
        }
    }
}
