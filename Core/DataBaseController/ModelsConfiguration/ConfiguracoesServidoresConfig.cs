using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseController.ModelsConfiguration
{
    internal sealed class ConfiguracoesServidoresConfig : IEntityTypeConfiguration<ConfiguracoesServidores>
    {
        public void Configure(EntityTypeBuilder<ConfiguracoesServidores> builder)
        {
            //Table
            builder.ToTable("ConfiguracoesServidoresAplicada");
            //Cod
            builder.HasKey(x => x.Cod);
            builder.Property(x => x.Cod).HasColumnName("cod").HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            //Servidor
            builder.HasOne(x => x.Servidor).WithMany(x => x.Configuracoes).HasForeignKey(x => x.CodServidor).IsRequired();
            //CodServidor
            builder.Property(x => x.CodServidor).HasColumnName("servidor");
            //Configuracoes
            builder.Property(x => x.Configuracoes).HasConversion<byte>().HasColumnName("configuracoes").HasColumnType("int").IsRequired();
            //Value
            builder.Property(x => x.Value).HasColumnName("valor").HasColumnType("text").HasCharSet("utf8mb4").IsRequired();
        }
    }
}
