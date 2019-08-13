using Domain.FuncionarioContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("FuncionarioId");

            builder.Property(c => c.Nome)
             .HasColumnName("Nome")
             .IsRequired()
             .HasColumnType("varchar(200)");

            builder.Property(c => c.SobreNome)
               .HasColumnName("SobreNome")
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(c => c.DataNascimento)
               .HasColumnName("DataNascimento");

            builder.Property(c => c.Nacionalidade)
              .HasColumnName("Nacionalidade")
              .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(c => c.Natural)
             .HasColumnName("Natural")
             .HasColumnType("varchar(200)");

            builder.Property(c => c.Email)
            .HasColumnName("Email")
            .IsRequired()
            .HasColumnType("varchar(200)");

            builder.Property(c => c.Foto)
            .HasColumnName("Foto")
            .HasColumnType("varchar(200)");

          

            builder.Property(c => c.Sexo)
              .HasColumnName("Sexo")
              .HasColumnType("varchar(200)");

            builder.Property(c => c.DataCadastro)
             .IsRequired();

            builder.Property(c => c.Status)
             .IsRequired();

            builder.Property(c => c.Rg)
             .HasColumnName("Rg")
             .HasColumnType("varchar(20)");

            builder.Property(c => c.Cpf)
               .HasColumnName("Cpf")
               .IsRequired()
               .HasColumnType("varchar(20)");

            builder.Property(c => c.TelefoneFixo)
             .HasColumnName("TelefoneFixo")
             .HasColumnType("varchar(15)");

            builder.Property(c => c.Celular)
              .HasColumnName("Celular")
               .IsRequired()
              .HasColumnType("varchar(15)");

            builder.Property(c => c.QuemCadastrouId)
              .HasColumnName("QuemCadastrou")
               .IsRequired()
               .ValueGeneratedOnAdd();


            builder.Property(c => c.UsuarioId)
              .HasColumnName("UsuarioId")
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Property(c => c.DadoPessoalId)
             .HasColumnName("DadoPessoalId")
              .IsRequired()
              .ValueGeneratedOnAdd();

            builder.Ignore(c => c.Notifications);
        }
    }
}
