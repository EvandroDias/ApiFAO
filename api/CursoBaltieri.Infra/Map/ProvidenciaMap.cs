using Domain.ProvidenciaContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class ProvidenciaMap : IEntityTypeConfiguration<Providencia>
    {
        public void Configure(EntityTypeBuilder<Providencia> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("ProvidenciaId");

            builder.Property(c => c.Titulo)
              .HasColumnName("Titulo")
              .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(c => c.Descricao)
             .HasColumnName("Descricao")
             .IsRequired();

            builder.Property(c => c.DataProvidencia)
             .HasColumnName("DataProvidencia")
             .IsRequired();

            builder.Property(c => c.DataCadastro)
            .HasColumnName("DataCadastro")
            .IsRequired();


            builder.Property(c => c.Status)
           .HasColumnName("Status")
           .IsRequired();
     
            builder.Property(c => c.FuncionarioId)
              .HasColumnName("FuncionarioId")
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Property(c => c.OcorrenciaId)
              .HasColumnName("OcorrenciaId")
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Ignore(c => c.Notifications);
        }
    }
}
