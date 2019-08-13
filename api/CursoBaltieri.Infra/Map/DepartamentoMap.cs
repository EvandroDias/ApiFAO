using Domain.DepartamentoContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class DepartamentoMap : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("DepartamentoId");

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
