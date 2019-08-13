using Domain.OcorrenciaContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class OcorrenciaMap : IEntityTypeConfiguration<Ocorrencia>
    {
        public void Configure(EntityTypeBuilder<Ocorrencia> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("OcorrenciaId");

            builder.Property(c => c.Titulo)
              .HasColumnName("Titulo")
              .HasColumnType("varchar(200)");

            builder.Property(c => c.Descricao)
             .HasColumnName("Descricao");
             

            builder.Property(c => c.DataOcorrencia)
             .HasColumnName("DataOcorrencia")
             .IsRequired();

            builder.Property(c => c.Periodo)
             .HasColumnName("Periodo")
             .IsRequired();

            builder.Property(c => c.SerieId)
             .HasColumnName("SerieId")
              .IsRequired()
              .ValueGeneratedOnAdd();

            builder.Property(c => c.TipoOcorrenciaId)
            .HasColumnName("TipoOcorrenciaId")
              .IsRequired()
              .ValueGeneratedOnAdd();

            builder.Property(c => c.DataCadastro)
            .HasColumnName("DataCadastro")
            .IsRequired();


            builder.Property(c => c.Status)
           .HasColumnName("Status")
           .IsRequired();

            builder.Property(c => c.Visualizada)
                .HasColumnName("Visualizada")
                .IsRequired();

            builder.Property(c => c.FuncionarioId)
            .HasColumnName("FuncionarioId")
             .IsRequired()
             .ValueGeneratedOnAdd();

            builder.Property(c => c.UsuarioId)
              .HasColumnName("UsuarioId")
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Property(c => c.AlunoId)
              .HasColumnName("AlunoId")
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Ignore(c => c.Notifications);
        }
    }
}
