using Domain.AlunoContent.Entities;
using Domain.FuncionarioContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;
using System.Collections.Generic;

namespace Domain.DadoPessoalContent.Entities
{
    public class DadoPessoal : Entity
    {
        protected DadoPessoal()
        {

        }

        public DadoPessoal(string rua, string numero, string bairro, string uf, string cidade, string cep,string complemento = null)
        {
           
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Complemento = complemento;
            Uf = uf;
            Cidade = cidade;
            Cep = cep;
         
            this.DataCadastro = DataBrasilia.HorarioBrasilia(); 
        }

        public string Rua { get; private set; }
        public string Numero { get;private set; }
        public string Bairro { get; private set; }
        public string Complemento { get; private set; }
        public string Uf { get;private set; }
        public string Cidade { get;private set; }
        public string Cep { get; private set; }
         
        public DateTime DataCadastro { get; private set; }
        public IEnumerable<Aluno> Aluno { get; private set; }
        public IEnumerable<Funcionario> Funcionario { get; private set; }


        public void Alterar(string rua, string numero, string bairro, string uf, string cidade, string cep,string complemento = null)
        {
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Complemento = complemento;
            Uf = uf;
            Cidade = cidade;
            Cep = cep;
         
        }

      
    }
}
