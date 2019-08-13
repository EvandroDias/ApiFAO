using Domain.FuncaoContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class FuncaoMap : IEntityTypeConfiguration<Funcao>
    {
        public void Configure(EntityTypeBuilder<Funcao> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("FuncaoId");

            builder.Property(c => c.Nome)
              .HasColumnName("Nome")
              .IsRequired()
              .HasColumnType("varchar(200)");

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
