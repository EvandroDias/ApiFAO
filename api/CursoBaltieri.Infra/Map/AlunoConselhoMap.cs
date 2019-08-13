using Domain.AlunoConselhoContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class AlunoConselhoMap : IEntityTypeConfiguration<AlunoConselho>
    {
        public void Configure(EntityTypeBuilder<AlunoConselho> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("AlunoConselhoId");

            builder.Property(c => c.Descricao)
             .HasColumnName("Descricao")
             .IsRequired();

            builder.Property(c => c.AlunoId)
              .HasColumnName("AlunoId")
             .IsRequired()
             .ValueGeneratedOnAdd();

            builder.Property(c => c.ConselhoId)
             .HasColumnName("ConselhoId")
            .IsRequired()
            .ValueGeneratedOnAdd();
                       

            builder.Property(c => c.UsuarioId)
           .HasColumnName("UsuarioId")
            .IsRequired()
            .ValueGeneratedOnAdd();

       


            builder.Property(c => c.DataCadastro)
             .IsRequired();

            

            builder.Ignore(c => c.Notifications);
        }
    }
}
