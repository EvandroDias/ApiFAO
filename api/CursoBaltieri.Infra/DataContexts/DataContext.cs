using Domain.AlunoConselhoContent.Entities;
using Domain.AlunoContent.Entities;
using Domain.AnoContent.Entities;
using Domain.BimestreContent.Entities;
using Domain.ConselhoContent.Entities;
using Domain.DadoPessoalContent.Entities;
using Domain.DepartamentoContent.Entities;
using Domain.DiaSemanaContent.Entities;
using Domain.EscolaContent.Entities;
using Domain.FuncaoContent.Entities;
using Domain.FuncionarioContent.Entities;
using Domain.HorarioRotinaContent.Entities;
using Domain.ItemAlunoTurmaContent.Entities;
using Domain.ItemDepartamentoEscolaContent.Entities;
using Domain.ItemDisciplinaTurmaContent.Entities;
using Domain.OcorrenciaContent.Entities;
using Domain.ProvidenciaContent.Entities;
using Domain.RotinaContent.Entities;
using Domain.SerieContent.Entities;
using Domain.TipoOcorrenciaContent.Entities;
using Domain.TurmaContent.Entities;
using Domain.UserContent.Entities;
using Infra.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace Infra.DataContexts
{
    public class DataContext : DbContext
    {


        //public DataContext()
        //{
        //    Configuration.LazyLoadingEnabled = false;
        //    Configuration.ProxyCreationEnabled = false;
        //}

        //public DataContext(DbContextOptions<DataContext> opcoes)
        //    : base(opcoes)
        //{

        //}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<TipoOcorrencia> TipoOcorrencias { get; set; }
        public DbSet<DadoPessoal> DadoPessoals { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Funcao> Funcaos { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<ItemDepartamentoEscola> ItemDepartamentoEscolas { get; set; }
        public DbSet<ItemAlunoTurma> ItemAlunoTurmas { get; set; }
        public DbSet<ItemDisciplinaTurma> ItemDisciplinaTurmas { get; set; }
        public DbSet<Providencia> Providencias { get; set; }
        public DbSet<Rotina> Rotinas { get; set; }
        public DbSet<HorarioRotina> HorarioRotinas { get; set; }
        public DbSet<DiaSemana> DiaSemanas { get; set; }
        public DbSet<Bimestre> Bimestres { get; set; }
        public DbSet<AlunoConselho> AlunoConselhos { get; set; }
        public DbSet<Conselho> Conselhos { get; set; }
        public DbSet<Ano> Anos { get; set; }

        public DbSet<Turma> Turmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            

            //UserMap.ConfiguraCliente(optionsBuilder);
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new DadoPessoalMap());
            modelBuilder.ApplyConfiguration(new SerieMap());
            modelBuilder.ApplyConfiguration(new DisciplinaMap());
            modelBuilder.ApplyConfiguration(new DepartamentoMap());
            modelBuilder.ApplyConfiguration(new EscolaMap());
            modelBuilder.ApplyConfiguration(new FuncaoMap());
            modelBuilder.ApplyConfiguration(new AlunoMap());
            modelBuilder.ApplyConfiguration(new FuncionarioMap());
            modelBuilder.ApplyConfiguration(new ItemDepartamentoEscolaMap());
            modelBuilder.ApplyConfiguration(new ItemAlunoTurmaMap());
            modelBuilder.ApplyConfiguration(new ItemDisciplinaTurmaMap());
            modelBuilder.ApplyConfiguration(new OcorrenciaMap());
            modelBuilder.ApplyConfiguration(new TipoOcorrenciaMap());
            modelBuilder.ApplyConfiguration(new ProvidenciaMap());
            modelBuilder.ApplyConfiguration(new RotinaMap());
            modelBuilder.ApplyConfiguration(new DiaSemanaMap());
            modelBuilder.ApplyConfiguration(new HorarioRotinaMap());
            modelBuilder.ApplyConfiguration(new BimestreMap());
            modelBuilder.ApplyConfiguration(new ConselhoMap());
            modelBuilder.ApplyConfiguration(new AlunoConselhoMap());
            modelBuilder.ApplyConfiguration(new AnoMap());
            modelBuilder.ApplyConfiguration(new TurmaMap());

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .AddEnvironmentVariables();
            //var configuration = builder.Build();

            //var sqlConnectionString =
            //    configuration["DataAccessPostgreSqlProvider:ConnectionString"];

            //optionsBuilder.UseSqlServer(sqlConnectionString);
        }
    }
}

