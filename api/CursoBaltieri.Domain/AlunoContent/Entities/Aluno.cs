using Domain.DadoPessoalContent.Entities;
using Domain.ItemAlunoTurmaContent.Entities;
using Domain.OcorrenciaContent.Entities;
using Domain.PessoaContent.Entities;
using Domain.UserContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.AlunoContent.Entities
{
    public class Aluno : Pessoa
    {
        protected Aluno()
        {

        }

        
        public Aluno(string nome, string sobreNome, DateTime dataNascimento, string sexo, string nacionalidade, string natural, Guid dadoPessoalId,string ra,string rm,string corRaca,string gemeos,Guid usuarioId) : base(nome, sobreNome, dataNascimento, sexo, nacionalidade,  natural,  dadoPessoalId)
        {
            this.UsuarioId = usuarioId;
           // this.DadoPessoalId = dadoPessoalId;
            this.Rm = rm;
            this.Ra = ra;
            this.Gemeos = gemeos;
            this.RacaCor = corRaca;
            this.AtivarDesativar();
        }

        public void AlterarAluno(string nome, string sobreNome, DateTime dataNascimento, string sexo, string nacionalidade, string natural,string rm,string ra,string gemeos,string racaCor)
        {

            this.Alterar(nome, sobreNome, dataNascimento, sexo, nacionalidade, natural);
            Rm = rm;
            Ra = ra;
            Gemeos = gemeos;
            RacaCor = racaCor;

        }

        public string Rm { get; private set; }
        public string Ra { get; private set; }
        public string Gemeos { get; private set; }
        public string RacaCor { get; private set; }
       

        public Guid UsuarioId { get; private set; }
       // public Guid  DadoPessoalId{ get; private set; }


        public Usuario Usuario { get; private set; }
        public DadoPessoal DadoPessoal { get; private set; }

        public IEnumerable<Ocorrencia> Ocorrencia { get; private set; }

        public IEnumerable<ItemAlunoTurma> ItemAlunoTurma { get; private set; }


    }
}
