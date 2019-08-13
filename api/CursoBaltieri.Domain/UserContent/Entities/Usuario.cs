using Domain.AlunoContent.Entities;
using Domain.DadoPessoalContent.Entities;
using Domain.EscolaContent.Entities;
using Domain.FuncaoContent.Entities;
using Domain.OcorrenciaContent.Entities;
using Domain.SerieContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;
using System.Collections.Generic;

namespace Domain.UserContent.Entities
{
    public class Usuario : Entity
    {
        protected Usuario()
        {
        }
    

        public Usuario(string nome,string sobreNome, string login, string senha,string email)
        {
            Nome = nome.ToUpper();
            SobreNome = sobreNome.ToUpper();
            Senha = senha;
            DataCadastro = DataBrasilia.HorarioBrasilia();
            Login = login;
            Status = true;
            DataRecuperacao = DataBrasilia.HorarioBrasilia(); ;
            UltimoAcesso = DataBrasilia.HorarioBrasilia();
            Email = email.ToLower();

        }

        public string Nome { get; private set; }
        public string SobreNome { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime UltimoAcesso { get; private set; }
        public bool Status { get; private set; }
        public string RecuperarSenha { get; private set; }
        public DateTime DataRecuperacao { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string ChaveSenha { get;private set; }
        public string TipoUsuario { get;private set; }
        public string Foto { get; private set; }
        public bool ConfigDadoPessoal { get; private set; }
        public string Email { get; private set; }




        public IEnumerable<Disciplina> Disciplina { get; private set; }
        public IEnumerable<Serie> Serie { get; private set; }
        public IEnumerable<Escola> Escola { get; private set; }
        public IEnumerable<Funcao> Funcao { get; private set; }
        public IEnumerable<Aluno> Aluno { get; private set; }
        public IEnumerable<Ocorrencia> Ocorrencia { get; private set; }

        public void SetarConfigDadoPessoal()
        {
            this.ConfigDadoPessoal = true;
        }

        public void Alterar(string nome,string sobrenome,string login)
        {
            this.Nome = nome.ToUpper();
            this.SobreNome = sobrenome.ToUpper();
            this.Login = login;
        }


        public void AtivarDesativar()
        {
            this.Status = !this.Status;
        }

      

        public override string ToString()
        {
            return this.Nome + " " + this.SobreNome;
        }

        public void TrocarSenha(string senhaNova,string chaveSenha)
        {
            
            this.Senha = senhaNova;
            this.ChaveSenha = chaveSenha;
          
        }

        public void SetarChaveSenha(string chaveSenha,string senha)
        {
            this.ChaveSenha = chaveSenha;
            this.Senha = senha;
        }

        

        public void SetarFoto()
        {
            this.Foto = "user.jpg";
        }

        public void SetarUltimoAcesso()
        {
            this.UltimoAcesso = DataBrasilia.HorarioBrasilia();
        }

        public void GerarRecuperSenha()
        {
            this.DataRecuperacao = DateTime.Now;
            this.RecuperarSenha = Guid.NewGuid().ToString();
        }


        public void SetarConfiDadoPessoal()
        {
            this.ConfigDadoPessoal = true;
        }

        public void SetarTipoUsuario(string tipoUsuario)
        {
            this.TipoUsuario = tipoUsuario;
        }


    }
}
