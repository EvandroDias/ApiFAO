using Domain.SerieContent.Entities;
using Domain.TurmaContent.Entities;
using FluentValidator;
using Shared.Util;
using System;

namespace Domain.ItemDisciplinaTurmaContent.Entities
{
    public class ItemDisciplinaTurma :  Notifiable
    {
        public ItemDisciplinaTurma(string turmaId, string disciplinaId)
        {
            TurmaId = Guid.Parse(turmaId);
            DisciplinaId = Guid.Parse(disciplinaId);
            DataCadastro = DataBrasilia.HorarioBrasilia();
        }

        protected ItemDisciplinaTurma()
        {

        }

        public void Alterar(string turmaId, string disciplinaId)
        {
            TurmaId = Guid.Parse(turmaId);
            DisciplinaId = Guid.Parse(disciplinaId);
        }

        public Guid DisciplinaId {get; private set; }
        public Guid TurmaId { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public Turma Turma { get; private set; }
        public Disciplina Disciplina { get; private set; }
    }
}
