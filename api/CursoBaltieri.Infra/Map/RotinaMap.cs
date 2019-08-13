using Domain.RotinaContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class RotinaMap : IEntityTypeConfiguration<Rotina>
    {
        public void Configure(EntityTypeBuilder<Rotina> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("RotinaId");

            builder.Property(c => c.De)
              .HasColumnName("De")
              .IsRequired();

            builder.Property(c => c.Ate)
              .HasColumnName("Ate")
              .IsRequired();

            builder.Property(c => c.DataCadastro)
              .HasColumnName("DataCadastro")
              .IsRequired();


            builder.Property(c => c.ImgCabecalho)
              .HasColumnName("ImgCabecalho")
              .HasColumnType("nvarchar(200)")
              .IsRequired();


            builder.Property(c => c.Status)
            .HasColumnName("Status")
            .IsRequired();
           

            builder.Property(c => c.FuncionarioId)
              .HasColumnName("FuncionarioId")
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Ignore(c => c.Notifications);
        }
    }
}
