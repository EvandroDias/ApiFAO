using Domain.EscolaContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class EscolaMap : IEntityTypeConfiguration<Escola>
    {
        public void Configure(EntityTypeBuilder<Escola> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("EscolaId");

            builder.Property(c => c.Nome)
              .HasColumnName("Nome")
              .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(c => c.UsuarioId)
              .HasColumnName("UsuarioId")
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Ignore(c => c.Notifications);
        }
    }
}
