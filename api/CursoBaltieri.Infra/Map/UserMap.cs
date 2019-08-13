using Domain.UserContent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public  class UserMap : IEntityTypeConfiguration<Usuario>
    {
        //public  void ConfiguraCliente(EntityTypeBuilder<User> builder)
        //{

          
        //    //obj.Entity<User>(u =>
        //    //{
        //    //    u.ToTable("User");
        //    //    u.HasKey(c => c.Id);
        //    //    u.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
        //    //    u.Property(c => c.Login).HasMaxLength(40);
        //    //    u.Property(c => c.Address).HasMaxLength(200);
        //    //    u.Property(c => c.Status);
        //    //    u.Property(c => c.Senha).IsRequired().HasMaxLength(200);
        //    //    u.Property(c => c.DateRegister);

        //    //});
        //}

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasColumnName("UsuarioId");

            builder.Property(c => c.Nome)
               .HasColumnName("Nome")
                .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(c => c.Email)
              .HasColumnName("Email")
               .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(c => c.SobreNome)
             .HasColumnName("SobreNome")
              .IsRequired()
             .HasColumnType("varchar(200)");

            builder.Property(c => c.Foto)
            .HasColumnName("Foto")
            .HasColumnType("varchar(200)");


            builder.Property(c => c.Login)
               .HasColumnName("Login")
                .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(c => c.Senha)
             .HasColumnName("Senha")
             .IsRequired()
             .HasColumnType("varchar(200)");

            builder.Property(c => c.TipoUsuario)
             .HasColumnName("TipoUsuario")
             .IsRequired()
             .HasColumnType("varchar(200)");

            builder.Property(c => c.DataCadastro)
             .HasColumnName("DataCadastro")
             .IsRequired()
             .HasColumnType("DateTime");

            builder.Property(c => c.DataRecuperacao)
             .HasColumnName("DataRecuperacao")
             .HasColumnType("DateTime");

            builder.Property(c => c.RecuperarSenha)
            .HasColumnName("RecuperarSenha")
            .HasColumnType("varchar(200)");

            builder.Property(c => c.Status)
             .HasColumnName("Status")
             .IsRequired();

          
            builder.Property(c => c.ConfigDadoPessoal)
            .HasColumnName("ConfigDadoPessoal")
            .IsRequired();

            builder.Property(c => c.ChaveSenha)
              .HasColumnName("ChaveSenha")
               .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Ignore(c => c.Notifications);
        }
    }
}
