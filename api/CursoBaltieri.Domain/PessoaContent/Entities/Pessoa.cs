using Shared.Entities;
using Shared.Util;
using System;

namespace Domain.PessoaContent.Entities
{
    public class Pessoa : Entity
    {
        protected Pessoa()
        {

        }
        
        public Pessoa(string nome, string sobreNome,DateTime dataNascimento, string sexo, string nacionalidade, string natural,  Guid dadoPessoalId)
        {
            Nome = nome;
            SobreNome = sobreNome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Nacionalidade = nacionalidade;
            Natural = natural;
            DadoPessoalId = dadoPessoalId;
            this.DataCadastro = DataBrasilia.HorarioBrasilia();
            
        }

               

        public string Nome { get; private set; }
        public string SobreNome { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Status { get; private set; }

        public string Rg { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Sexo { get; private set; }
        public string TelefoneFixo { get; private set; }
        public string Celular { get; private set; }
        public string Nacionalidade { get; private set; }
        public string Natural { get; private set; }
        public string Email { get; private set; }
        public string Foto { get; private set; }
        
        public Guid DadoPessoalId { get; private set; }




        public void Alterar(string nome, string sobreNome, DateTime dataNascimento, string sexo,string nacionalidade, string natural)
        {
            Nome = nome;
            SobreNome = sobreNome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Nacionalidade = nacionalidade;
            Natural = natural;
            
        }

        public void SetarRgCpf(string rg,string cpf)
        {
            this.Rg = rg;
            this.Cpf = cpf;
        }

        public void SetarCelular(string numero)
        {
            this.Celular = numero;
        }

        public void SetarTelefoneFixo(string numero)
        {
            this.TelefoneFixo = numero;
        }

        public void SetarEmail(string email)
        {
            this.Email = email;
        }

        public void SetarFoto(string foto)
        {
            this.Foto = foto;
        }

        public void AtivarDesativar()
        {
            this.Status = !Status;
        }
                     

    }
}
