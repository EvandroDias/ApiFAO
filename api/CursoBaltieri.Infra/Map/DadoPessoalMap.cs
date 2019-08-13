using Domain.DadoPessoalContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class DadoPessoalMap : IEntityTypeConfiguration<DadoPessoal>
    {
        public void Configure(EntityTypeBuilder<DadoPessoal> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("DadoPessoalId");

            builder.Property(c => c.Rua)
               .HasColumnName("Rua")
                //.IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(c => c.Numero)
              .HasColumnName("Numero")
               //.IsRequired()
              .HasColumnType("varchar(20)");

            builder.Property(c => c.Bairro)
              .HasColumnName("Bairro")
               //.IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(c => c.Cep)
              .HasColumnName("Cep")
               //.IsRequired()
              .HasColumnType("varchar(10)");

            builder.Property(c => c.Uf)
              .HasColumnName("Uf")
               .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(c => c.Cidade)
              .HasColumnName("Cidade")
               .IsRequired()
              .HasColumnType("varchar(200)");

                      

           
         

            builder.Property(c => c.Complemento)
              .HasColumnName("Complemento")
              .HasColumnType("varchar(200)");

            builder.Property(c => c.DataCadastro)
              .HasColumnName("DataCadastro")
               .IsRequired();
              




            //builder.Property(c => c.UsuarioId)
            //  .HasColumnName("UsuarioId")
            //   .IsRequired()
            //   .ValueGeneratedOnAdd();

            builder.Ignore(c => c.Notifications);



        }
    }
}
