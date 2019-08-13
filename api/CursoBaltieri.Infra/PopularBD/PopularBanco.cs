using Domain.AnoContent.Entities;
using Domain.BimestreContent.Entities;
using Domain.DadoPessoalContent.Entities;
using Domain.DepartamentoContent.Entities;
using Domain.DiaSemanaContent.Entities;
using Domain.EscolaContent.Entities;
using Domain.FuncaoContent.Entities;
using Domain.FuncionarioContent.Entities;
using Domain.FuncionarioContent.Handlers;
using Domain.SerieContent.Entities;
using Domain.TipoOcorrenciaContent.Entities;
using Domain.UserContent.Entities;
using Infra.DataContexts;
using Shared.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.PopularBD
{
    public class PopularBanco
    {
        private readonly DataContext _context;



        public PopularBanco(DataContext context)
        {
            _context = context;


        }

        public void Initialize()
        {
            if (_context.Database.EnsureCreated())
             {

            

            var usuario = CriarUsuario("Evandro", "Dias Cassimiro", "221.921.428-11", "cckkattra", "evandrodiascassimiro@gmail.com");

            var dadoPessoal = CriarDadoPessoal("Tocantins", "210", "Jd São Luis", "SP", "Estrela d`Oeste", "15650-000", "casa");


            var funcao1 = new Funcao("Professor(a)", usuario.Id);
            var funcao2 = new Funcao("Coordenador(a)", usuario.Id);
            var funcao3 = new Funcao("Secretário(a)", usuario.Id);
            var funcao4 = new Funcao("Estagiário(a)", usuario.Id);
            var funcao5 = new Funcao("Inspetor(a)", usuario.Id);
            var funcao6 = new Funcao("Bibliotecária", usuario.Id);
            var funcao7 = new Funcao("Cozinheira", usuario.Id);
            var funcao8 = new Funcao("Diretor(a)", usuario.Id);
            var funcao9 = new Funcao("Faxina", usuario.Id);
            var funcao10 = new Funcao("Instrutor de Informática", usuario.Id);

                var bimestres = new List<Bimestre>()
                {
                    new Bimestre("1° Bimestre",usuario.Id),
                    new Bimestre("2° Bimestre",usuario.Id),
                    new Bimestre("3° Bimestre",usuario.Id),
                    new Bimestre("4° Bimestre",usuario.Id),
                   
                };

                var anos = new List<Ano>()
                {
                    new Ano("2019",usuario.Id),
                    new Ano("2020",usuario.Id),
                    new Ano("2021",usuario.Id),
                    new Ano("2022",usuario.Id),
                    new Ano("2023",usuario.Id),
                    new Ano("2024",usuario.Id),
                    new Ano("2025",usuario.Id),
                    new Ano("2026",usuario.Id),
                    new Ano("2027",usuario.Id),
                    new Ano("2028",usuario.Id),
                    new Ano("2029",usuario.Id),
                    new Ano("2030",usuario.Id),

                };



            CriarEscola(usuario.Id, "EMF Francisco Alves de Oliveira(FAO)");
            CriarFuncao(funcao1);
            CriarFuncao(funcao2);
            CriarFuncao(funcao3);
            CriarFuncao(funcao4);
            CriarFuncao(funcao5);
            CriarFuncao(funcao6);
            CriarFuncao(funcao7);
            CriarFuncao(funcao8);
            CriarFuncao(funcao9);
            CriarFuncao(funcao10);

            CriarFuncionario(dadoPessoal.Id, usuario.Id, funcao10.Id);
            CriarSerie(usuario.Id);
            CriarDisciplina(usuario.Id);
            CriarTipoOcorrencia();
            CriarDepartamento(usuario.Id);
            CriarDiaSemana();
            CriarBimestre(bimestres);
            CriarAno(anos);

            _context.SaveChanges();

            }
        }

        public void CriarAno(List<Ano> anos)
        {
            foreach (var item in anos)
            {
                _context.Anos.Add(item);
            }
        }

        public void CriarBimestre(List<Bimestre> bimestres)
        {
            foreach (var item in bimestres)
            {
                _context.Bimestres.Add(item);
            }
        }

        public DadoPessoal CriarDadoPessoal(string rua, string numero, string bairro, string uf, string cidade, string cep, string complemento)
        {
            var dadoPessoal = new DadoPessoal(rua, numero, bairro, uf, cidade, cep, complemento);
            _context.DadoPessoals.Add(dadoPessoal);

            return dadoPessoal;
        }

        public Usuario CriarUsuario(string nome, string sobreNome, string cpf, string senha, string email)
        {
            var usuario = new Usuario(nome, sobreNome, cpf, senha, email);
            var chaveSenha = Functions.GetRandomString();
            var _senha = Crypto.EncriptarSenha(senha, chaveSenha);
            usuario.SetarChaveSenha(chaveSenha, _senha);
            usuario.SetarFoto();
            usuario.SetarTipoUsuario("Administrador");
            _context.Usuarios.Add(usuario);

            return usuario;
        }



        public void CriarFuncao(Funcao funcao)
        {
            _context.Funcaos.Add(funcao);
        }



        public void CriarFuncionario(Guid dadoPessoalId, Guid usuarioId, Guid funcaoId)
        {
            var funcionario = new Funcionario("Evandro", "Dias Cassimiro", new DateTime(1981, 09, 17), "Masculino", "brasileira", "Estrela d`Oeste", dadoPessoalId, usuarioId, usuarioId, funcaoId);
            funcionario.SetarCelular("(17)99605-6382");
            funcionario.SetarEmail("evandrodiascassimiro@gmail.com");
            funcionario.SetarRgCpf("28771896-7", "221.921.428-11");
            funcionario.SetarTelefoneFixo(null);
            funcionario.AtivarDesativar();
            _context.Funcionarios.Add(funcionario);
        }

        public void CriarDiaSemana()
        {
            var letras = new string[5];
            letras[0] = "Segunda";
            letras[1] = "Terça";
            letras[2] = "Quarta";
            letras[3] = "Quinta";
            letras[4] = "Sexta";
            

           
                for (int x = 0; x < 5; x++)
                {
                    var s = new DiaSemana(letras[x]);
                    _context.DiaSemanas.Add(s);
                }

            

        }

        public void CriarSerie(Guid usuarioId)
        {
            var letras = new string[5];
            letras[0] = "A";
            letras[1] = "B";
            letras[2] = "C";
            letras[3] = "D";
            letras[4] = "E";

            for (int i = 1; i < 6; i++)
            {
                for (int x = 0; x < 5; x++)
                {
                    var s = new Serie(i + "° Série " + letras[x],i, usuarioId);
                    _context.Series.Add(s);
                }

            }

        }

        public void CriarEscola(Guid usuarioId,string nome)
        {
            var escola = new Escola(nome, usuarioId);
            _context.Escolas.Add(escola);
        }

        public void CriarDepartamento(Guid usuarioId)
        {
            var d = new string[25];
            d[0] = "Informática";
            d[1] = "Secretária";
            d[2] = "Coordenação";
            d[3] = "Sala dos Professores";
            d[4] = "Cozinha";
            d[5] = "Depósito";
            d[6] = "Sala 1";
            d[7] = "Sala 2";
            d[8] = "Sala 3";
            d[9] = "Sala 4";
            d[10] = "Sala 5";
            d[11] = "Sala 6";
            d[12] = "Sala 7";
            d[13] = "Sala 8";
            d[14] = "Sala 9";
            d[15] = "Sala 10";
            d[16] = "Sala 11";
            d[17] = "Sala 12";
            d[18] = "Sala 13";
            d[19] = "Sala 14";
            d[20] = "Sala 15";
            d[21] = "Biblioteca";
            d[22] = "Diretoria";
            d[23] = "Recurso";
            d[24] = "Reforço";

            for (int i = 0; i < d.Length; i++)
            {
                var s = new Departamento(d[i], usuarioId);
                _context.Departamentos.Add(s);

            }

        }

        public void CriarDisciplina(Guid usuarioId)
        {
            var d = new string[9];
            d[0] = "Matemática";
            d[1] = "Português";
            d[2] = "Inglês";
            d[3] = "História";
            d[4] = "Geografia";
            d[5] = "Educação Física";
            d[6] = "Artes";
            d[7] = "Informática";
            d[8] = "Leitura";

            for (int i = 0; i < d.Length; i++)
            {
                var s = new Disciplina(d[i], usuarioId);
                _context.Disciplinas.Add(s);

            }

        }

        public void CriarTipoOcorrencia()
        {
            var t = new string[4];
            t[0] = "Faltas";
            t[1] = "Não Fez Tarefa";
            t[2] = "Brigas / Agressões";
            t[3] = "Esqueceu Material";

            for (int i = 0; i < t.Length; i++)
            {
                var s = new TipoOcorrencia(t[i]);
                _context.TipoOcorrencias.Add(s);

            }

        }

    }
}
