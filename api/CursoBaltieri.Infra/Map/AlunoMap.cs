using Domain.AlunoContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("AlunoId");

            builder.Property(c => c.Nome)
             .HasColumnName("Nome")
             .IsRequired()
             .HasColumnType("varchar(200)");

            builder.Property(c => c.SobreNome)
               .HasColumnName("SobreNome")
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(c => c.Rm)
               .HasColumnName("Rm")
               .IsRequired()
               .HasColumnType("varchar(20)");

            builder.Property(c => c.Ra)
               .HasColumnName("Ra")
               .IsRequired()
               .HasColumnType("varchar(20)");

            builder.Property(c => c.Nacionalidade)
              .HasColumnName("Nacionalidade")
              .HasColumnType("varchar(200)");

            builder.Property(c => c.Natural)
               .HasColumnName("Natural")
               .HasColumnType("varchar(200)");

            builder.Property(c => c.Sexo)
               .HasColumnName("Sexo")
                .HasColumnType("varchar(200)");


            builder.Property(c => c.RacaCor)
               .HasColumnName("RacaCor")
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(c => c.Gemeos)
               .HasColumnName("Gemeos")
               .IsRequired()
               .HasColumnType("varchar(10)");

            builder.Property(c => c.UsuarioId)
           .HasColumnName("UsuarioId")
            .IsRequired()
            .ValueGeneratedOnAdd();

            builder.Property(c => c.DadoPessoalId)
             .HasColumnName("DadoPessoalId")
              .IsRequired()
              .ValueGeneratedOnAdd();


            builder.Property(c => c.DataCadastro)
             .IsRequired();

            builder.Ignore(c => c.Rg);


            builder.Ignore(c => c.Cpf);
          

            builder.Property(c => c.Status)
             .IsRequired();

         

            builder.Ignore(c => c.Notifications);
        }
    }
}
