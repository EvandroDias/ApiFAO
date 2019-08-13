using Domain.DadoPessoalContent.Entities;
using Domain.FuncaoContent.Entities;
using Domain.OcorrenciaContent.Entities;
using Domain.PessoaContent.Entities;
using Domain.TurmaContent.Entities;
using Domain.UserContent.Entities;
using System;
using System.Collections.Generic;

namespace Domain.FuncionarioContent.Entities
{
    public class Funcionario : Pessoa
    {
        protected Funcionario()
        {

        }


        public Funcionario(string nome, string sobreNome, DateTime dataNascimento, string sexo, string nacionalidade, string natural, Guid dadoPessoalId, Guid usuarioId,Guid quemCadastrou,Guid funcaoId) : base(nome, sobreNome,  dataNascimento, sexo, nacionalidade, natural, dadoPessoalId)
        {
            this.UsuarioId = usuarioId;
            this.QuemCadastrouId = quemCadastrou;
            this.FuncaoId = funcaoId;
         
        }

        public Guid UsuarioId { get; private set; }
        public Guid QuemCadastrouId { get; private set; }
        public Guid FuncaoId { get; private set; }
        

        public Usuario Usuario { get; private set; }
        public Funcao Funcao { get; private set; }
        public DadoPessoal DadoPessoal { get; private set; }
        public IEnumerable<Turma> Turma { get; private set; }

    }
}
