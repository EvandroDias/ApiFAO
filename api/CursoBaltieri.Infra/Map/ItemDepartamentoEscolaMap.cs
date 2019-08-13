using Domain.ItemDepartamentoEscolaContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class ItemDepartamentoEscolaMap : IEntityTypeConfiguration<ItemDepartamentoEscola>
    {
        public void Configure(EntityTypeBuilder<ItemDepartamentoEscola> builder)
        {
            builder.HasKey(c => new {c.EscolaId,c.DepartamentoId });

            builder.Property(c => c.EscolaId)
               .ValueGeneratedOnAdd()
               .HasColumnName("EscolaId");

            builder.Property(c => c.DepartamentoId)
               .ValueGeneratedOnAdd()
               .HasColumnName("DepartamentoId");

            builder.Property(c => c.DataCadastro)
              .HasColumnName("DataCadastro")
              .IsRequired();

         
             

            builder.Ignore(c => c.Notifications);
        }
    }
}
