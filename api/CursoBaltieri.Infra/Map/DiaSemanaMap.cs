using Domain.DiaSemanaContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class DiaSemanaMap : IEntityTypeConfiguration<DiaSemana>
    {
        public void Configure(EntityTypeBuilder<DiaSemana> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("DiaSemanaId");

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
