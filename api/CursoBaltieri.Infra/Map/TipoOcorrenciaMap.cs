using Domain.TipoOcorrenciaContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class TipoOcorrenciaMap : IEntityTypeConfiguration<TipoOcorrencia>
    {
        public void Configure(EntityTypeBuilder<TipoOcorrencia> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("TipoOcorrenciaId");

            builder.Property(c => c.Nome)
              .HasColumnName("Nome")
              .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(c => c.Status)
            .HasColumnName("Status")
            .IsRequired();
           
                        

            builder.Ignore(c => c.Notifications);
        }
    }
}
