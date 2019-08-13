using Domain.TurmaContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class TurmaMap : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("TurmaId");

            builder.Property(c => c.AnoId)
              .HasColumnName("AnoId")
              .IsRequired()
              .ValueGeneratedOnAdd();

            builder.Property(c => c.Coordenador)
             .HasColumnName("Coordenador")
             .IsRequired()
             .HasColumnType("nvarchar(200)");

            builder.Property(c => c.DepartamentoId)
            .HasColumnName("DepartamentoId")
            .IsRequired()
            .ValueGeneratedOnAdd();

            builder.Property(c => c.Diretor)
              .HasColumnName("Diretor")
              .IsRequired()
              .HasColumnType("nvarchar(200)");

            builder.Property(c => c.Ensino)
             .HasColumnName("Ensino")
             .IsRequired()
             .HasColumnType("nvarchar(200)");

            builder.Property(c => c.EscolaId)
            .HasColumnName("EscolaId")
            .IsRequired()
            .ValueGeneratedOnAdd();

            builder.Property(c => c.FuncionarioId)
              .HasColumnName("FuncionarioId")
              .IsRequired()
              .ValueGeneratedOnAdd();

            builder.Property(c => c.Periodo)
            .HasColumnName("Periodo")
            .IsRequired()
            .HasColumnType("nvarchar(200)");

            builder.Property(c => c.QtdAulas1Bimestre)
             .HasColumnName("QtdAulas1Bimestre")
             .IsRequired();

            builder.Property(c => c.QtdAulas2Bimestre)
            .HasColumnName("QtdAulas2Bimestre")
            .IsRequired();

            builder.Property(c => c.QtdAulas3Bimestre)
            .HasColumnName("QtdAulas3Bimestre")
            .IsRequired();

            builder.Property(c => c.QtdAulas4Bimestre)
            .HasColumnName("QtdAulas4Bimestre")
            .IsRequired();


            builder.Property(c => c.Status)
            .HasColumnName("Status")
            .IsRequired();
           

            builder.Property(c => c.UsuarioId)
              .HasColumnName("UsuarioId")
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Ignore(c => c.Notifications);
        }
    }
}
