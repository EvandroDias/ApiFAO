using Domain.ItemAlunoTurmaContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class ItemAlunoTurmaMap : IEntityTypeConfiguration<ItemAlunoTurma>
    {
        public void Configure(EntityTypeBuilder<ItemAlunoTurma> builder)
        {
            builder.HasKey(c => new {c.AlunoId,c.TurmaId });

            builder.Property(c => c.AlunoId)
               .ValueGeneratedOnAdd()
               .HasColumnName("AlunoId");

            builder.Property(c => c.TurmaId)
               .ValueGeneratedOnAdd()
               .HasColumnName("TurmaId");

            builder.Property(c => c.DataCadastro)
              .HasColumnName("DataCadastro")
              .IsRequired();

            builder.Property(c => c.Numero)
                .HasColumnName("Numero")
                .IsRequired();

            builder.Property(c => c.Status)
               .HasColumnName("Status")
               .HasColumnType("nvarchar(200)")
               .IsRequired();


            builder.Ignore(c => c.Notifications);
        }
    }
}
