using Domain.AlunoContent.Entities;
using Domain.TurmaContent.Entities;
using FluentValidator;
using Shared.Util;
using System;

namespace Domain.ItemAlunoTurmaContent.Entities
{
    public class ItemAlunoTurma :  Notifiable
    {
        public ItemAlunoTurma(string turmaId, string alunoId,int numero)
        {
            TurmaId = Guid.Parse(turmaId);
            AlunoId = Guid.Parse(alunoId);
            Numero = numero;
            Status = "Ativo";
            DataCadastro = DataBrasilia.HorarioBrasilia();
        }

        protected ItemAlunoTurma()
        {

        }

        public void Alterar(string turmaId, string alunoId,int numero,string status)
        {
            TurmaId = Guid.Parse(turmaId);
            AlunoId = Guid.Parse(alunoId);
            Numero = numero;
            Status = status;
        }

        public void SetarStatus(string status)
        {
            Status = status;
        }

        public Guid AlunoId {get; private set; }
        public Guid TurmaId { get; private set; }
        public int Numero { get; private set; }
        public string Status { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public Turma Turma { get; private set; }
        public Aluno Aluno { get; private set; }
    }
}
