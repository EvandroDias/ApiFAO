using Domain.AlunoConselhoContent.Commands.Outputs;
using System;
using System.Collections.Generic;

namespace Domain.ConselhoContent.Commands.Outputs
{
    public class ListarConselhoResults
    {
        public ListarConselhoResults()
        {
            this.Alunos = new List<ListarAlunoConselhoResults>();
        }

        public ListarConselhoResults(DateTime dataConselho,DateTime dataCadastro,string nomeSerie, string nomeAno, string nomeFuncionario, string nomeBimestre,string nomeCoordenador,string nomeDiretor,List<ListarAlunoConselhoResults> alunos)
        {            
            DataCadastro = dataCadastro;
            NomeSerie = nomeSerie;
            NomeAno = nomeAno;
            NomeFuncionario = nomeFuncionario;
            NomeBimestre = nomeBimestre;
            NomeCoordenador = nomeCoordenador;
            NomeDiretor = nomeDiretor;
            Alunos = alunos;
            DataConselho = dataConselho;
        }

        public Guid ConselhoId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataConselho { get; set; }
        public Guid FuncionarioId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid AlunoId { get; set; }
        public Guid SerieId { get; set; }
        public DateTime DataCadastro { get; set; }
        public Boolean Status { get; set; }

        public string NomeAluno { get; set; }
        public string NomeSerie { get; set; }
        public string NomeAno { get; set; }
        public string NomeFuncionario { get; set; }

        public string NomeDiretor { get; set; }
        public string NomeBimestre { get; set; }
        public Guid BimestreId { get; set; }
        public Guid AnoId { get; set; }
        public string NomeCoordenador { get; set; }

        public List<ListarAlunoConselhoResults> Alunos { get; set; }
    }
}
