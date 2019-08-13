using Domain.ItemDisciplinaTurmaContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class ItemDisciplinaTurmaMap : IEntityTypeConfiguration<ItemDisciplinaTurma>
    {
        public void Configure(EntityTypeBuilder<ItemDisciplinaTurma> builder)
        {
            builder.HasKey(c => new {c.DisciplinaId,c.TurmaId });

            builder.Property(c => c.DisciplinaId)
               .ValueGeneratedOnAdd()
               .HasColumnName("DisciplinaId");

            builder.Property(c => c.TurmaId)
               .ValueGeneratedOnAdd()
               .HasColumnName("TurmaId");

            builder.Property(c => c.DataCadastro)
              .HasColumnName("DataCadastro")
              .IsRequired();

         
             

            builder.Ignore(c => c.Notifications);
        }
    }
}
