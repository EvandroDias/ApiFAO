using Domain.HorarioRotinaContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class HorarioRotinaMap : IEntityTypeConfiguration<HorarioRotina>
    {
        public void Configure(EntityTypeBuilder<HorarioRotina> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("HorarioRotinaId");

            builder.Property(c => c.Conteudo)
              .HasColumnName("Conteudo")
              .IsRequired();

            builder.Property(c => c.Aula)
             .HasColumnName("Aula")
             .HasColumnType("nvarchar(200)")
             .IsRequired();

            builder.Property(c => c.DataCadastro)
              .HasColumnName("DataCadastro")
              .IsRequired();

            builder.Property(c => c.DiaSemanaId)
             .HasColumnName("DiaSemanaId")
             .IsRequired()
            .ValueGeneratedOnAdd();

            builder.Property(c => c.RotinaId)
            .HasColumnName("RotinaId")
            .IsRequired()
           .ValueGeneratedOnAdd();


            builder.Property(c => c.Status)
            .HasColumnName("Status")
            .IsRequired();
                                   

            builder.Ignore(c => c.Notifications);
        }
    }
}
