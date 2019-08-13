using Domain.ConselhoContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class ConselhoMap : IEntityTypeConfiguration<Conselho>
    {
        public void Configure(EntityTypeBuilder<Conselho> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("ConselhoId");

           


            builder.Property(c => c.NomeCoordenador)
                .IsRequired()
               .HasColumnName("NomeCoordenador");


            builder.Property(c => c.DataConselho)
             .HasColumnName("DataConselho")
             .IsRequired();

          

            builder.Property(c => c.SerieId)
             .HasColumnName("SerieId")
              .IsRequired()
              .ValueGeneratedOnAdd();

            builder.Property(c => c.BimestreId)
            .HasColumnName("BimestreId")
              .IsRequired()
              .ValueGeneratedOnAdd();

            builder.Property(c => c.DataCadastro)
            .HasColumnName("DataCadastro")
            .IsRequired();


            builder.Property(c => c.Status)
           .HasColumnName("Status")
           .IsRequired();
                       

            builder.Property(c => c.FuncionarioId)
            .HasColumnName("FuncionarioId")
             .IsRequired()
             .ValueGeneratedOnAdd();

            builder.Property(c => c.UsuarioId)
              .HasColumnName("UsuarioId")
               .IsRequired()
               .ValueGeneratedOnAdd();

          

            builder.Ignore(c => c.Notifications);
        }
    }
}
