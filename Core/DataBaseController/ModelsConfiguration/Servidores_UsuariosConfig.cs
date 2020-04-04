using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseController.ModelsConfiguration
{
    internal sealed class Servidores_UsuariosConfig : IEntityTypeConfiguration<Servidores_Usuarios>
    {
        public void Configure(EntityTypeBuilder<Servidores_Usuarios> builder)
        {
            //Table
            builder.ToTable("Servidores_Usuarios");
            builder.HasKey(x => new { x.Servidor, x.Usuario });
            //Servidor
            builder.HasOne(x => x.Servidor).WithMany(x => x.ServidoresUsuarios).HasForeignKey("Servidores_codigo_servidor");
            //Usuario
            builder.HasOne(x => x.Usuario).WithMany(x => x.ServidoresUsuarios).HasForeignKey("Usuarios_codigo_usuario");
        }
    }
}
